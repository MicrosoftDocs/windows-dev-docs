---
author: stevewhims
description: You can use both Windows.Networking.Sockets and Winsock to communicate with other devices as a Universal Windows Platform (UWP) app developer.
title: Sockets
ms.assetid: 23B10A3C-E33F-4CD6-92CB-0FFB491472D6
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# Sockets


**Important APIs**

-   [**Windows.Networking.Sockets**](https://msdn.microsoft.com/library/windows/apps/br226960)
-   [Winsock](https://msdn.microsoft.com/library/windows/desktop/ms740673)

You can use both [**Windows.Networking.Sockets**](https://msdn.microsoft.com/library/windows/apps/br226960) and [Winsock](https://msdn.microsoft.com/library/windows/desktop/ms737523) to communicate with other devices as a Universal Windows Platform (UWP) app developer. This topic provides in-depth guidance on using the **Windows.Networking.Sockets** namespace to perform networking operations.

>**Note**
>As part of [network isolation](https://msdn.microsoft.com/library/windows/apps/hh770532.aspx), the system forbids establishing socket connections (Sockets or WinSock) between two UWP apps running on the same machine via either the local loopback address (127.0.0.0) or explicitly specifying the local IP address. This means that you cannot use sockets to communicate between two UWP apps. UWP supplies other mechanisms for communicating between apps. See [App-to-app communications](https://msdn.microsoft.com/windows/uwp/app-to-app/index) for details.

## Basic TCP socket operations

A TCP socket provides low-level network data transfers in either direction for long-lived connections. TCP sockets are the underlying feature used by most of the network protocols used on the Internet. This section shows how to enable a UWP app to send and receive data with a TCP stream socket using the [**StreamSocket**](https://msdn.microsoft.com/library/windows/apps/br226882) and [**StreamSocketListener**](https://msdn.microsoft.com/library/windows/apps/br226906) classes as part of the [**Windows.Networking.Sockets**](https://msdn.microsoft.com/library/windows/apps/br226960) namespace. For this section, we will be creating a very simple app that will function as an echo server and client to demonstrate basic TCP operations.

**Creating a TCP echo server**

The following code example demonstrates how to create a [**StreamSocketListener**](https://msdn.microsoft.com/library/windows/apps/br226906) object and start listening for incoming TCP connections.

```csharp
try
{
    //Create a StreamSocketListener to start listening for TCP connections.
    Windows.Networking.Sockets.StreamSocketListener socketListener = new Windows.Networking.Sockets.StreamSocketListener();

    //Hook up an event handler to call when connections are received.
    socketListener.ConnectionReceived += SocketListener_ConnectionReceived;

    //Start listening for incoming TCP connections on the specified port. You can specify any port that' s not currently in use.
    await socketListener.BindServiceNameAsync("1337");
}
catch (Exception e)
{
    //Handle exception.
}
```

The following code example implements the SocketListener\_ConnectionReceived event handler that was attached to the [**StreamSocketListener.ConnectionReceived**](https://msdn.microsoft.com/library/windows/apps/hh701494) event in the above example. This event handler is called by the [**StreamSocketListener**](https://msdn.microsoft.com/library/windows/apps/br226906) class every time a remote client has established a connection with the echo server.

```csharp
private async void SocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender, 
    Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
{
    //Read line from the remote client.
    Stream inStream = args.Socket.InputStream.AsStreamForRead();
    StreamReader reader = new StreamReader(inStream);
    string request = await reader.ReadLineAsync();
    
    //Send the line back to the remote client.
    Stream outStream = args.Socket.OutputStream.AsStreamForWrite();
    StreamWriter writer = new StreamWriter(outStream);
    await writer.WriteLineAsync(request);
    await writer.FlushAsync();
}
```

**Creating a TCP echo client**

The following code example demonstrates how to create a [**StreamSocket**](https://msdn.microsoft.com/library/windows/apps/br226882) object, establish a connection to the remote server, send a request, and receive a response.

```csharp
try
{
    //Create the StreamSocket and establish a connection to the echo server.
    Windows.Networking.Sockets.StreamSocket socket = new Windows.Networking.Sockets.StreamSocket();
    
    //The server hostname that we will be establishing a connection to. We will be running the server and client locally,
    //so we will use localhost as the hostname.
    Windows.Networking.HostName serverHost = new Windows.Networking.HostName("localhost");
    
    //Every protocol typically has a standard port number. For example HTTP is typically 80, FTP is 20 and 21, etc.
    //For the echo server/client application we will use a random port 1337.
    string serverPort = "1337";
    await socket.ConnectAsync(serverHost, serverPort);

    //Write data to the echo server.
    Stream streamOut = socket.OutputStream.AsStreamForWrite();
    StreamWriter writer = new StreamWriter(streamOut);
    string request = "test";
    await writer.WriteLineAsync(request);
    await writer.FlushAsync();

    //Read data from the echo server.
    Stream streamIn = socket.InputStream.AsStreamForRead();
    StreamReader reader = new StreamReader(streamIn);
    string response = await reader.ReadLineAsync();
}
catch (Exception e)
{
    //Handle exception here.            
}
```

## Basic UDP socket operations

A UDP socket provides low-level network data transfers in either direction for network communication that does not require an established connection. Because UDP sockets do not maintain connection on both endpoints they provide fast and simple solution for networking between remote machines. However, UDP sockets do not ensure integrity of the network packets or whether they make it to the remote destination at all. Some examples of applications that use UDP sockets are local network discovery and local chat clients. This section demonstrates the use of the [**DatagramSocket**](https://msdn.microsoft.com/library/windows/apps/br241319) class to sending and receiving UDP messages by creating a simple echo server and client.

**Creating a UDP echo server**

The following code example demonstrates how to create a [**DatagramSocket**](https://msdn.microsoft.com/library/windows/apps/br241319) object and bind it to a specific port so that you can listen for incoming UDP messages.

```csharp
Windows.Networking.Sockets.DatagramSocket socket = new Windows.Networking.Sockets.DatagramSocket();

socket.MessageReceived += Socket_MessageReceived;

//You can use any port that is not currently in use already on the machine.
string serverPort = "1337";

//Bind the socket to the serverPort so that we can start listening for UDP messages from the UDP echo client.
await socket.BindServiceNameAsync(serverPort);
```

The following code example implements the **Socket\_MessageReceived** event handler to read a message that was received from a client and send the same message back.

```csharp
private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
{
    //Read the message that was received from the UDP echo client.
    Stream streamIn = args.GetDataStream().AsStreamForRead();
    StreamReader reader = new StreamReader(streamIn);
    string message = await reader.ReadLineAsync();

    //Create a new socket to send the same message back to the UDP echo client.
    Windows.Networking.Sockets.DatagramSocket socket = new Windows.Networking.Sockets.DatagramSocket();
    
    //Use a separate port number for the UDP echo client because both will be unning on the same machine.
    string clientPort = "1338"
    Stream streamOut = (await socket.GetOutputStreamAsync(args.RemoteAddress, clientPort)).AsStreamForWrite();
    StreamWriter writer = new StreamWriter(streamOut);
    await writer.WriteLineAsync(message);
    await writer.FlushAsync();
}
```

**Creating a UDP echo client**

The following code example demonstrates how to create a [**DatagramSocket**](https://msdn.microsoft.com/library/windows/apps/br241319) object and bind it to a specific port so that you can listen for incoming UDP messages and send a UDP message to the UDP echo server.

```csharp
private async void testUdpSocketServer()
{
    Windows.Networking.Sockets.DatagramSocket socket = new Windows.Networking.Sockets.DatagramSocket();
    
    socket.MessageReceived += Socket_MessageReceived;
    
    //You can use any port that is not currently in use already on the machine. We will be using two separate and random 
    //ports for the client and server because both the will be running on the same machine.
    string serverPort = "1337";
    string clientPort = "1338";
    
    //Because we will be running the client and server on the same machine, we will use localhost as the hostname.
    Windows.Networking.HostName serverHost = new Windows.Networking.HostName("localhost");
    
    //Bind the socket to the clientPort so that we can start listening for UDP messages from the UDP echo server.
    await socket.BindServiceNameAsync(clientPort);
                
    //Write a message to the UDP echo server.
    Stream streamOut = (await socket.GetOutputStreamAsync(serverHost, serverPort)).AsStreamForWrite();
    StreamWriter writer = new StreamWriter(streamOut);
    string message = "Hello, world!";
    await writer.WriteLineAsync(message);
    await writer.FlushAsync();
}
```

The following code example implements the **Socket\_MessageReceived** event handler to read a message that was received from the UDP echo server.

```csharp
private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, 
    Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
{
    //Read the message that was received from the UDP echo server.
    Stream streamIn = args.GetDataStream().AsStreamForRead();
    StreamReader reader = new StreamReader(streamIn);
    string message = await reader.ReadLineAsync();
}
```

## Background operations and the socket broker

If your app receives connections or data on sockets, then you must be prepared to perform those operations properly while your app is not in the foreground. To do so, you use the socket broker. For more information on how to use the socket broker, see [Network communications in the background](network-communications-in-the-background.md).

## Batched sends

Starting with Windows 10, Windows.Networking.Sockets supports batched sends, a way for you to send multiple buffers of data together with much lower context-switching overhead than if you send each of the buffers separately. This is especially useful if your app is doing VoIP, VPN, or other tasks which involve moving a lot of data as efficiently as possible.

Each call to WriteAsync on a socket triggers a kernel transition to reach the network stack. When an app writes many buffers at a time, each write incurs a separate kernel transition, and this creates substantial overhead. The new batched sends pattern optimizes the frequency of kernel transitions. This functionality is currently limited to [**StreamSocket**](https://msdn.microsoft.com/library/windows/apps/br226882) and connected [**DatagramSocket**](https://msdn.microsoft.com/library/windows/apps/br241319) instances.

Here is an example of how an app would send a large number of buffers in a non-optimal way.

```csharp
// Send a set of packets inefficiently, one packet at a time.
// This is not recommended if you have many packets to send
IList<IBuffer> packetsToSend = PreparePackets();
var outputStream = stream.OutputStream;

foreach (IBuffer packet in packetsToSend)
{
    // Incurs kernel transition overhead for each packet
    await outputStream.WriteAsync(packet);
}
```

This example shows a more efficient way to send a large number of buffers. Because this technique uses features unique to the C# language, it is only available to C# programmers. By sending multiple packets at a time, this example enables the system to batch sends, and thus optimize kernel transitions for improved performance.

```csharp
// More efficient way to send packets.
// This way enables the system to do batched sends
IList<IBuffer> packetsToSend = PreparePackets();
var outputStream = stream.OutputStream;

int i = 0;
Task[] pendingTasks = new Tast[packetsToSend.Count];
foreach (IBuffer packet in packetsToSend)
{
    // track all pending writes as tasks, but do not wait on one before peforming the next
    pendingTasks[i++] = outputStream.WriteAsync(packet).AsTask();
    // Do not modify any buffer' s contents until the pending writes are complete.
}
// Now, wait for all of the pending writes to complete
await Task.WaitAll(pendingTasks);
```

This example shows another way to send a large number of buffers in a way that's compatible with batched sends. And since it doesn't use any C#-specific features, it is applicable for all languages (though it is demonstrated here in C#). Instead, it uses changed behavior in the **OutputStream** member of the [**StreamSocket**](https://msdn.microsoft.com/library/windows/apps/br226882) and [**DatagramSocket**](https://msdn.microsoft.com/library/windows/apps/br241319) classes that is new in Windows 10.

```csharp
// More efficient way to send packets in Windows 10, using the new behavior of OutputStream.FlushAsync().
int i = 0;
IList<IBuffer> packetsToSend = PreparePackets();
var outputStream = socket.OutputStream;

var pendingWrites = new IAsyncOperationWithProgress<uint,uint> [packetsToSend.Count];

foreach (IBuffer packet in packetsToSend)
{
    pendingWrites[i++] = outputStream.WriteAsync(packet);
    // Do not modify any buffer' s contents until the pending writes are complete.
}

// Wait for all pending writes to complete. This step enables batched sends on the output stream.
await outputStream.FlushAsync();
```

In earlier versions of Windows, **FlushAsync** returned immediately, and did not guarantee that all operations on the stream had completed yet. In Windows 10, the behavior has changed. **FlushAsync** is now guaranteed to return after all operations on the output stream have completed.

There are some important limitations imposed by using batched writes in your code.

-   You cannot modify the contents of the **IBuffer** instances being written until the asynchronous write is complete.
-   The **FlushAsync** pattern only works on **StreamSocket.OutputStream** and **DatagramSocket.OutputStream**.
-   The **FlushAsync** pattern only works in Windows 10 and onward.
-   In other cases, use **Task.WaitAll** instead of the **FlushAsync** pattern.

## Port sharing for DatagramSocket

Windows 10 introduces a new [**DatagramSocketControl**](https://msdn.microsoft.com/library/windows/apps/hh701190) property, [**MulticastOnly**](https://msdn.microsoft.com/library/windows/apps/dn895368), which enables you to specify that the **DatagramSocket** in question is able to coexist with other Win32 or WinRT multicast sockets bound to the same address/port.

## Providing a client certificate with the StreamSocket class

The [**Windows.Networking.StreamSocket**](https://msdn.microsoft.com/library/windows/apps/br226882) class supports using SSL/TLS to authenticate the server the app is talking to. In certain cases, the app also needs to authenticate itself to the server using a TLS client certificate. In Windows 10, you can provide a client certificate on the [**StreamSocket.Control**](https://msdn.microsoft.com/library/windows/apps/br226893) object (this must be set before the TLS handshake is started). If the server requests the client certificate, Windows will respond with the certificate provided.

Here is a code snippet showing how to implement this:

```csharp
var socket = new StreamSocket();
Windows.Security.Cryptography.Certificates.Certificate certificate = await GetClientCert();
socket.Control.ClientCertificate = certificate;
await socket.ConnectAsync(destination, SocketProtectionLevel.Tls12);
```

## Exceptions in Windows.Networking.Sockets

The constructor for the [**HostName**](https://msdn.microsoft.com/library/windows/apps/br207113) class used with sockets can throw an exception if the string passed is not a valid hostname (contains characters that are not allowed in a host name). If an app gets input from the user for the **HostName**, the constructor should be in a try/catch block. If an exception is thrown, the app can notify the user and request a new hostname.

The [**Windows.Networking.Sockets**](https://msdn.microsoft.com/library/windows/apps/br226960) namespace has convenient helper methods and enumerations for handling errors when using sockets and WebSockets. This can be useful for handling specific network exceptions differently in your app.

An error encountered on [**DatagramSocket**](https://msdn.microsoft.com/library/windows/apps/br241319), [**StreamSocket**](https://msdn.microsoft.com/library/windows/apps/br226882), or [**StreamSocketListener**](https://msdn.microsoft.com/library/windows/apps/br226906) operation is returned as an **HRESULT** value. The [**SocketError.GetStatus**](https://msdn.microsoft.com/library/windows/apps/hh701462) method is used to convert a network error from a socket operation to a [**SocketErrorStatus**](https://msdn.microsoft.com/library/windows/apps/hh701457) enumeration value. Most of the **SocketErrorStatus** enumeration values correspond to an error returned by the native Windows sockets operation. An app can filter on specific **SocketErrorStatus** enumeration values to modify app behavior depending on the cause of the exception.

An error encountered on a [**MessageWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226842) or [**StreamWebSocket**](https://msdn.microsoft.com/library/windows/apps/br226923) operation is returned as an **HRESULT** value. The [**WebSocketError.GetStatus**](https://msdn.microsoft.com/library/windows/apps/hh701529) method is used to convert a network error from a WebSocket operation to a [**WebErrorStatus**](https://msdn.microsoft.com/library/windows/apps/hh747818) enumeration value. Most of the **WebErrorStatus** enumeration values correspond to an error returned by the native HTTP client operation. An app can filter on specific **WebErrorStatus** enumeration values to modify app behavior depending on the cause of the exception.

For parameter validation errors, an app can also use the **HRESULT** from the exception to learn more detailed information on the error that caused the exception. Possible **HRESULT** values are listed in the *Winerror.h* header file. For most parameter validation errors, the **HRESULT** returned is **E\_INVALIDARG**.

## The Winsock API

You can use [Winsock](https://msdn.microsoft.com/library/windows/desktop/ms740673) in your UWP app, as well. The supported Winsock API is based on that of Windows Phone 8.1Microsoft Silverlight and continues to support most of the types, properties and methods (some APIs that are considered obsolete have been removed). You can find more information on Winsock programming [here](https://msdn.microsoft.com/library/windows/desktop/ms740673).


