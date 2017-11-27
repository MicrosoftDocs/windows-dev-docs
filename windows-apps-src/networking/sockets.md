---
author: stevewhims
description: Sockets are a low-level data transfer technology on top of which many networking protocols are implemented. UWP offers TCP and UDP socket classes for client-server or peer-to-peer applications, whether connections are long-lived or an established connection is not required.
title: Sockets
ms.assetid: 23B10A3C-E33F-4CD6-92CB-0FFB491472D6
ms.author: stwhi
ms.date: 11/26/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# Sockets

Sockets are a low-level data transfer technology on top of which many networking protocols are implemented. UWP offers TCP and UDP socket classes for client-server or peer-to-peer applications, whether connections are long-lived or an established connection is not required.

This topic focuses on how to use the Universal Windows Platform (UWP) socket classes that are in the [**Windows.Networking.Sockets**](/uwp/api/Windows.Networking.Sockets?branch=live) namespace. But you can also use [Windows Sockets 2 (Winsock)](https://msdn.microsoft.com/library/windows/desktop/ms740673) in a UWP app.

> [!NOTE]
> As a consequence of [network isolation](https://msdn.microsoft.com/library/windows/apps/hh770532.aspx), Windows disallows establishing a socket connection (Sockets or WinSock) between two UWP apps running on the same machine; whether that's via the local loopback address (127.0.0.0), or by explicitly specifying the local IP address. For details about mechanisms by which UWP apps can communicate with one another, see [App-to-app communication](/windows/uwp/app-to-app/index?branch=live).

## Build a basic TCP socket client and server

A TCP (Transmission Control Protocol) socket provides low-level network data transfers in either direction for connections that are long-lived. TCP sockets are the underlying feature used by most of the network protocols used on the Internet. To demonstrate basic TCP operations, the example code below shows a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket?branch=live) and a [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener?branch=live) sending and receiving data over TCP to form an echo client and server.

To begin with as few moving parts as possible&mdash;and to sidestep network isolation issues for the present&mdash;create a new project, and put both the client and the server code below into the same project.

You'll need to [declare an app capability](../packaging/app-capability-declarations.md) in your project. Open your app package manifest source file (the `Package.appxmanifest` file) and, on the Capabilities tab, check **Private Networks (Client & Server)**. This is how that looks in the `Package.appxmanifest` markup.

```xml
<Capability Name="privateNetworkClientServer" />
```

Instead of `privateNetworkClientServer`, you can declare `internetClientServer` if you're connecting over the internet. Both **StreamSocket** and **StreamSocketListener** need one or other of these app capabilities to be declared.

### An echo client and server, using TCP sockets

Construct a [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener?branch=live) and begin listening for incoming TCP connections. The [**StreamSocketListener.ConnectionReceived**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener?branch=live#Windows_Networking_Sockets_StreamSocketListener_ConnectionReceived) event is raised each time a client establishes a connection with the **StreamSocketListener**.

Also construct a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket?branch=live), establish a connection to the server, send a request, and receive a response.

Create a new **Page** named `StreamSocketAndListenerPage`. Put the XAML markup in `StreamSocketAndListenerPage.xaml`, and the put the imperative code inside the `StreamSocketAndListenerPage` class.

```XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
	<Grid.RowDefinitions>
		<RowDefinition Height="Auto"/>
		<RowDefinition Height="*"/>
	</Grid.RowDefinitions>

	<StackPanel>
		<TextBlock Margin="9.6,0" Style="{StaticResource TitleTextBlockStyle}" Text="TCP socket example"/>
		<TextBlock Margin="7.2,0,0,0" Style="{StaticResource HeaderTextBlockStyle}" Text="StreamSocket &amp; StreamSocketListener"/>
	</StackPanel>

	<Grid Grid.Row="1">
		<Grid.RowDefinitions>
			<RowDefinition/>
			<RowDefinition/>
		</Grid.RowDefinitions>
		<Grid.ColumnDefinitions>
			<ColumnDefinition Width="*"/>
			<ColumnDefinition Width="*"/>
		</Grid.ColumnDefinitions>
		<TextBlock Margin="9.6" Style="{StaticResource SubtitleTextBlockStyle}" Text="client"/>
		<ListBox x:Name="clientListBox" Grid.Row="1" Margin="9.6"/>
		<TextBlock Grid.Column="1" Margin="9.6" Style="{StaticResource SubtitleTextBlockStyle}" Text="server"/>
		<ListBox x:Name="serverListBox" Grid.Column="1" Grid.Row="1" Margin="9.6"/>
	</Grid>
</Grid>
```

```csharp
// Every protocol typically has a standard port number. For example, HTTP is typically 80, FTP is 20 and 21, etc.
// For this example, we'll choose an arbitrary port number.
static string PortNumber = "1337";

protected override void OnNavigatedTo(NavigationEventArgs e)
{
	this.StartServer();
	this.StartClient();
}

private async void StartServer()
{
	try
	{
		var streamSocketListener = new Windows.Networking.Sockets.StreamSocketListener();

		// The ConnectionReceived event is raised when connections are received.
		streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived;

		// Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
		await streamSocketListener.BindServiceNameAsync(StreamSocketAndListenerPage.PortNumber);

		this.serverListBox.Items.Add("server is listening...");
	}
	catch (Exception ex)
	{
		Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
		this.serverListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
	}
}

private async void StreamSocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender, Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
{
	string request;
	using (var streamReader = new StreamReader(args.Socket.InputStream.AsStreamForRead()))
	{
		request = await streamReader.ReadLineAsync();
	}

	await this.serverListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.serverListBox.Items.Add(string.Format("server received the request: \"{0}\"", request));
	});

	// Echo the request back as the response.
	using (Stream outputStream = args.Socket.OutputStream.AsStreamForWrite())
	{
		using (var streamWriter = new StreamWriter(outputStream))
		{
			await streamWriter.WriteLineAsync(request);
			await streamWriter.FlushAsync();
		}
	}

	await this.serverListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.serverListBox.Items.Add(string.Format("server sent back the response: \"{0}\"", request));
	});

	sender.Dispose();

	await this.serverListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.serverListBox.Items.Add("server closed its socket");
	});
}

private async void StartClient()
{
	try
	{
		// Create the StreamSocket and establish a connection to the echo server.
		using (var streamSocket = new Windows.Networking.Sockets.StreamSocket())
		{
			// The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
			var hostName = new Windows.Networking.HostName("localhost");

			this.clientListBox.Items.Add("client is trying to connect...");

			await streamSocket.ConnectAsync(hostName, StreamSocketAndListenerPage.PortNumber);

			this.clientListBox.Items.Add("client connected");

			// Send a request to the echo server.
			string request = "Hello, World!";
			using (Stream outputStream = streamSocket.OutputStream.AsStreamForWrite())
			{
				using (var streamWriter = new StreamWriter(outputStream))
				{
					await streamWriter.WriteLineAsync(request);
					await streamWriter.FlushAsync();
				}
			}

			this.clientListBox.Items.Add(string.Format("client sent the request: \"{0}\"", request));

			// Read data from the echo server.
			string response;
			using (Stream inputStream = streamSocket.InputStream.AsStreamForRead())
			{
				using (StreamReader streamReader = new StreamReader(inputStream))
				{
					response = await streamReader.ReadLineAsync();
				}
			}

			this.clientListBox.Items.Add(string.Format("client received the response: \"{0}\" ", response));
		}

		this.clientListBox.Items.Add("client closed its socket");
	}
	catch (Exception ex)
	{
		Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
		this.clientListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
	}
}
```

## Build a basic UDP socket client and server

A UDP (User Datagram Protocol) socket is similar to a TCP socket in that it also provides low-level network data transfers in either direction. But, while a TCP socket is for long-lived connections, a UDP socket is for applications where an established connection is not required. Because UDP sockets don't maintain connection on both endpoints, they're a fast and simple solution for networking between remote machines. But UDP sockets don't ensure integrity of the network packets nor even whether packets make it to the remote destination at all. So your app will need to be designed to tolerate that. Some examples of applications that use UDP sockets are local network discovery, and local chat clients.

To demonstrate basic UDP operations, the example code below shows the [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket?branch=live) class being used to both send and receive data over UDP to form an echo client and server. Create a new project, and put both the client and the server code below into the same project. Just as for a TCP socket, you'll need to declare the **Private Networks (Client & Server)** app capability.

### An echo client and server, using UDP sockets

Construct a [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket?branch=live) to play the role of the echo server, bind it to a specific port number, listen for an incoming UDP message, and echo it back. The [**DatagramSocket.MessageReceived**](/uwp/api/Windows.Networking.Sockets.DatagramSocket?branch=live#Windows_Networking_Sockets_DatagramSocket_MessageReceived) event is raised when a message is receieved on the socket.

Construct another **DatagramSocket** to play the role of the echo client, bind it to a specific port number, send a UDP message, and receive a response.

Put the XAML markup in `MainPage.xaml`, and the put the imperative code inside your `MainPage` class.

Create a new **Page** named `DatagramSocketPage`. Put the XAML markup in `DatagramSocketPage.xaml`, and the put the imperative code inside the `DatagramSocketPage` class.

```XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
	<Grid.RowDefinitions>
		<RowDefinition Height="Auto"/>
		<RowDefinition Height="*"/>
	</Grid.RowDefinitions>

	<StackPanel>
		<TextBlock Margin="9.6,0" Style="{StaticResource TitleTextBlockStyle}" Text="UDP socket example"/>
		<TextBlock Margin="7.2,0,0,0" Style="{StaticResource HeaderTextBlockStyle}" Text="DatagramSocket"/>
	</StackPanel>

	<Grid Grid.Row="1">
		<Grid.RowDefinitions>
			<RowDefinition/>
			<RowDefinition/>
		</Grid.RowDefinitions>
		<Grid.ColumnDefinitions>
			<ColumnDefinition Width="*"/>
			<ColumnDefinition Width="*"/>
		</Grid.ColumnDefinitions>
		<TextBlock Margin="9.6" Style="{StaticResource SubtitleTextBlockStyle}" Text="client"/>
		<ListBox x:Name="clientListBox" Grid.Row="1" Margin="9.6"/>
		<TextBlock Grid.Column="1" Margin="9.6" Style="{StaticResource SubtitleTextBlockStyle}" Text="server"/>
		<ListBox x:Name="serverListBox" Grid.Column="1" Grid.Row="1" Margin="9.6"/>
	</Grid>
</Grid>
```

```csharp
// Every protocol typically has a standard port number. For example, HTTP is typically 80, FTP is 20 and 21, etc.
// For this example, we'll choose different arbitrary port numbers for client and server, since both will be running on the same machine.
static string ClientPortNumber = "1336";
static string ServerPortNumber = "1337";

protected override void OnNavigatedTo(NavigationEventArgs e)
{
	this.StartServer();
	this.StartClient();
}

private async void StartServer()
{
	try
	{
		var serverDatagramSocket = new Windows.Networking.Sockets.DatagramSocket();

		// The ConnectionReceived event is raised when connections are received.
		serverDatagramSocket.MessageReceived += ServerDatagramSocket_MessageReceived;

		this.serverListBox.Items.Add("server is about to bind...");

		// Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
		await serverDatagramSocket.BindServiceNameAsync(DatagramSocketPage.ServerPortNumber);

		this.serverListBox.Items.Add(string.Format("server is bound to port number {0}", DatagramSocketPage.ServerPortNumber));
	}
	catch (Exception ex)
	{
		Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
		this.serverListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
	}
}

private async void ServerDatagramSocket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
{
	string request;
	using (DataReader dataReader = args.GetDataReader())
	{
		request = dataReader.ReadString(dataReader.UnconsumedBufferLength).Trim();
	}

	await this.serverListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.serverListBox.Items.Add(string.Format("server received the request: \"{0}\"", request));
	});

	// Echo the request back as the response.
	using (Stream outputStream = (await sender.GetOutputStreamAsync(args.RemoteAddress, DatagramSocketPage.ClientPortNumber)).AsStreamForWrite())
	{
		using (var streamWriter = new StreamWriter(outputStream))
		{
			await streamWriter.WriteLineAsync(request);
			await streamWriter.FlushAsync();
		}
	}

	await this.serverListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.serverListBox.Items.Add(string.Format("server sent back the response: \"{0}\"", request));
	});

	sender.Dispose();

	await this.serverListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.serverListBox.Items.Add("server closed its socket");
	});
}

private async void StartClient()
{
	try
	{
		// Create the DatagramSocket and establish a connection to the echo server.
		var clientDatagramSocket = new Windows.Networking.Sockets.DatagramSocket();

		clientDatagramSocket.MessageReceived += ClientDatagramSocket_MessageReceived;

		// The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
		var hostName = new Windows.Networking.HostName("localhost");

		this.clientListBox.Items.Add("client is about to bind...");

		await clientDatagramSocket.BindServiceNameAsync(DatagramSocketPage.ClientPortNumber);

		this.clientListBox.Items.Add(string.Format("client is bound to port number {0}", DatagramSocketPage.ClientPortNumber));

		// Send a request to the echo server.
		string request = "Hello, World!";
		using (var serverDatagramSocket = new Windows.Networking.Sockets.DatagramSocket())
		{
			using (Stream outputStream = (await serverDatagramSocket.GetOutputStreamAsync(hostName, DatagramSocketPage.ServerPortNumber)).AsStreamForWrite())
			{
				using (var streamWriter = new StreamWriter(outputStream))
				{
					await streamWriter.WriteLineAsync(request);
					await streamWriter.FlushAsync();
				}
			}
		}

		this.clientListBox.Items.Add(string.Format("client sent the request: \"{0}\"", request));
	}
	catch (Exception ex)
	{
		Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
		this.clientListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
	}
}

private async void ClientDatagramSocket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
{
	string response;
	using (DataReader dataReader = args.GetDataReader())
	{
		response = dataReader.ReadString(dataReader.UnconsumedBufferLength).Trim();
	}

	await this.clientListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.clientListBox.Items.Add(string.Format("client received the response: \"{0}\"", response));
	});

	sender.Dispose();

	await this.clientListBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
	{
		this.clientListBox.Items.Add("client closed its socket");
	});
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

## Important APIs

* [DatagramSocket](/uwp/api/Windows.Networking.Sockets.DatagramSocket?branch=live)
* [DatagramSocket.BindServiceNameAsync](/uwp/api/windows.networking.sockets.datagramsocket?branch=live#Windows_Networking_Sockets_DatagramSocket_BindServiceNameAsync_System_String_)
* [DatagramSocket.GetOutputStreamAsync](/uwp/api/windows.networking.sockets.datagramsocket?branch=live#Windows_Networking_Sockets_DatagramSocket_GetOutputStreamAsync_Windows_Networking_HostName_System_String_)
* [DatagramSocket.MessageReceived](/uwp/api/Windows.Networking.Sockets.DatagramSocket?branch=live#Windows_Networking_Sockets_DatagramSocket_MessageReceived)
* [DatagramSocketMessageReceivedEventArgs](/uwp/api/windows.networking.sockets.datagramsocketmessagereceivedeventargs?branch=live)
* [DatagramSocketMessageReceivedEventArgs.GetDataReader](/uwp/api/windows.networking.sockets.datagramsocketmessagereceivedeventargs?branch=live#Windows_Networking_Sockets_DatagramSocketMessageReceivedEventArgs_GetDataReader)
* [SocketError.GetStatus](/uwp/api/windows.networking.sockets.socketerror?branch=live#Windows_Networking_Sockets_SocketError_GetStatus_System_Int32_)
* [SocketErrorStatus](/uwp/api/windows.networking.sockets.socketerrorstatus?branch=live) 
* [StreamSocket](/uwp/api/Windows.Networking.Sockets.StreamSocket?branch=live)
* [StreamSocket.ConnectAsync](/uwp/api/windows.networking.sockets.streamsocket?branch=live#Windows_Networking_Sockets_StreamSocket_ConnectAsync_Windows_Networking_HostName_System_String_)
* [StreamSocket.InputStream](/uwp/api/windows.networking.sockets.streamsocket?branch=live#Windows_Networking_Sockets_StreamSocket_InputStream)
* [StreamSocket.OutputStream](/uwp/api/windows.networking.sockets.streamsocket?branch=live#Windows_Networking_Sockets_StreamSocket_OutputStream)
* [StreamSocketListener](/uwp/api/Windows.Networking.Sockets.StreamSocketListener?branch=live)
* [StreamSocketListener.BindServiceNameAsync](/uwp/api/windows.networking.sockets.streamsocketlistener?branch=live#Windows_Networking_Sockets_StreamSocketListener_BindServiceNameAsync_System_String_Windows_Networking_Sockets_SocketProtectionLevel_Windows_Networking_Connectivity_NetworkAdapter_)
* [StreamSocketListener.ConnectionReceived](/uwp/api/Windows.Networking.Sockets.StreamSocketListener?branch=live#Windows_Networking_Sockets_StreamSocketListener_ConnectionReceived)
* [StreamSocketListenerConnectionReceivedEventArgs](/uwp/api/windows.networking.sockets.streamsocketlistenerconnectionreceivedeventargs?branch=live)* [Windows.Networking.Sockets](/uwp/api/Windows.Networking.Sockets?branch=live)

## Related topics

* [Windows Sockets 2 (Winsock)](https://msdn.microsoft.com/library/windows/desktop/ms740673)
* [How to set network capabilities](https://msdn.microsoft.com/library/windows/apps/hh770532.aspx)
* [App-to-app communication](/windows/uwp/app-to-app/index?branch=live)
