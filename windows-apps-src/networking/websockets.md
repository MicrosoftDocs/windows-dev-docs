---
author: stevewhims
description: WebSockets provide a mechanism for fast, secure, two-way communication between a client and a server over the web using HTTP(S), and supporting both UTF-8 and binary messages.
title: WebSockets
ms.assetid: EAA9CB3E-6A3A-4C13-9636-CCD3DE46E7E2
ms.author: stwhi
ms.date: 11/26/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, networking, websocket, messagewebsocket, streamwebsocket
ms.localizationpriority: medium
---

# WebSockets

WebSockets provide a mechanism for fast, secure, two-way communication between a client and a server over the web using HTTP(S), and supporting both UTF-8 and binary messages.

Under the [WebSocket Protocol](http://tools.ietf.org/html/rfc6455), data is transferred immediately over a full-duplex single socket connection, allowing messages to be sent and received from both endpoints in real time. WebSockets are ideal for use in multiplayer gaming (both real-time and turn-based), instant social network notifications, up-to-date displays of stock or weather information, and other apps requiring secure and fast data transfer.

To establish a WebSocket connection, a specific, HTTP-based handshake is exchanged between the client and the server. If successful, the application-layer protocol is "upgraded" from HTTP to WebSockets, using the previously established TCP connection. Once this occurs, HTTP is completely out of the picture; data can be sent or received using the WebSocket protocol by both endpoints, until the WebSocket connection is closed.

**Note** A client cannot use WebSockets to transfer data unless the server also uses the WebSocket protocol. If the server does not support WebSockets, then you must use another method of data transfer.

The Universal Windows Platform (UWP) provides support for both client and server use of WebSockets. The [**Windows.Networking.Sockets**](/uwp/api/windows.networking.sockets?branch=live) namespace defines two WebSocket classes for use by clients&mdash;[**MessageWebSocket**](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live), and [**StreamWebSocket**](/uwp/api/windows.networking.sockets.streamwebsocket?branch=live). Here's a comparison of these two WebSocket classes.

| [MessageWebSocket](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live) | [StreamWebSocket](/uwp/api/windows.networking.sockets.streamwebsocket?branch=live) |
| - | - |
| An entire WebSocket message is read/written in a single operation. | Sections of a message can be read with each read operation. |
| Suitable when messages are not very large. | Suitable when very large files (such as photos or videos) are being transferred. |
| Supports both UTF-8 and binary messages. | Supports only binary messages. |
| Similar to a [UDP or datagram socket](sockets.md#build-a-basic-udp-socket-client-and-server) (in the sense of being intended for frequent, small messages), but with TCP's reliability, packet order guarantees, and congestion control. | Similar to a [TCP or stream socket](sockets.md#build-a-basic-tcp-socket-client-and-server). |

## Secure your connection with TLS/SSL

In most cases, you'll want to  use a secure WebSocket connection so that the data you send and receive is encrypted. This will also increase the chances that your connection will succeed, because many intermediaries such as firewalls and proxies reject unencrypted WebSocket connections. The [WebSocket protocol](https://tools.ietf.org/html/rfc6455#section-3) defines these two URI schemes.

| URI scheme | Purpose |
| - | - |
| wss: | Use for secure connections that should be encrypted. |
| ws: | Use for unencrypted connections. |

To encrypt your WebSocket connection, use the `wss:` URI scheme. Here's an example.

```csharp
protected override async void OnNavigatedTo(NavigationEventArgs e)
{
    var webSocket = new Windows.Networking.Sockets.MessageWebSocket();
    await webSocket.ConnectAsync(new Uri("wss://www.contoso.com/mywebservice"));
}
```

## Use MessageWebSocket to connect

[**MessageWebSocket**](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live) allows an entire WebSocket message to be read/written in a single operation. Consequently, it's suitable when messages are not very large. The class supports both UTF-8 and binary messages.

The example code below uses the WebSocket.org echo server&mdash;a service that echoes back to the sender any message sent to it.

```csharp
private Windows.Networking.Sockets.MessageWebSocket messageWebSocket;

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    this.messageWebSocket = new Windows.Networking.Sockets.MessageWebSocket();

    // In this example, we send/receive a string, so we need to set the MessageType to Utf8.
    this.messageWebSocket.Control.MessageType = Windows.Networking.Sockets.SocketMessageType.Utf8;

    this.messageWebSocket.MessageReceived += WebSocket_MessageReceived;
    this.messageWebSocket.Closed += WebSocket_Closed;

    try
    {
        Task connectTask = this.messageWebSocket.ConnectAsync(new Uri("wss://echo.websocket.org")).AsTask();
        connectTask.ContinueWith(_ => this.SendMessageUsingMessageWebSocketAsync("Hello, World!"));
    }
    catch (Exception ex)
    {
        Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
        // Add additional code here to handle exceptions.
    }
}

private async Task SendMessageUsingMessageWebSocketAsync(string message)
{
	using (var dataWriter = new DataWriter(this.messageWebSocket.OutputStream))
	{
		dataWriter.WriteString(message);
		await dataWriter.StoreAsync();
		dataWriter.DetachStream();
	}
	Debug.WriteLine("Sending message using MessageWebSocket: " + message);
}

private void WebSocket_MessageReceived(Windows.Networking.Sockets.MessageWebSocket sender, Windows.Networking.Sockets.MessageWebSocketMessageReceivedEventArgs args)
{
    try
    {
        using (DataReader dataReader = args.GetDataReader())
        {
            dataReader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            string message = dataReader.ReadString(dataReader.UnconsumedBufferLength);
            Debug.WriteLine("Message received from MessageWebSocket: " + message);
            this.messageWebSocket.Dispose();
        }
    }
    catch (Exception ex)
    {
        Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
        // Add additional code here to handle exceptions.
    }
}

private void WebSocket_Closed(Windows.Networking.Sockets.IWebSocket sender, Windows.Networking.Sockets.WebSocketClosedEventArgs args)
{
    Debug.WriteLine("WebSocket_Closed; Code: " + args.Code + ", Reason: \"" + args.Reason + "\"");
    // Add additional code here to handle the WebSocket being closed.
}
```

```cpp
#include <ppltasks.h>
#include <sstream>

	...
	
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Xaml::Navigation;

	...

private:
	Windows::Networking::Sockets::MessageWebSocket^ messageWebSocket;

protected:
	virtual void OnNavigatedTo(NavigationEventArgs^ e) override
	{
		this->messageWebSocket = ref new Windows::Networking::Sockets::MessageWebSocket();

		// In this example, we send/receive a string, so we need to set the MessageType to Utf8.
		this->messageWebSocket->Control->MessageType = Windows::Networking::Sockets::SocketMessageType::Utf8;

		this->messageWebSocket->MessageReceived += ref new TypedEventHandler<Windows::Networking::Sockets::MessageWebSocket^, Windows::Networking::Sockets::MessageWebSocketMessageReceivedEventArgs^>(this, &MessageWebSocketPage::WebSocket_MessageReceived);
		this->messageWebSocket->Closed += ref new TypedEventHandler<Windows::Networking::Sockets::IWebSocket^, Windows::Networking::Sockets::WebSocketClosedEventArgs^>(this, &MessageWebSocketPage::WebSocket_Closed);

		try
		{
			auto connectTask = Concurrency::create_task(this->messageWebSocket->ConnectAsync(ref new Uri(L"wss://echo.websocket.org")));
			connectTask.then([this] { this->SendMessageUsingMessageWebSocketAsync(L"Hello, World!"); });
		}
		catch (Platform::Exception^ ex)
		{
			Windows::Web::WebErrorStatus webErrorStatus = Windows::Networking::Sockets::WebSocketError::GetStatus(ex->HResult);
			// Add additional code here to handle exceptions.
		}
	}

private:
	void SendMessageUsingMessageWebSocketAsync(Platform::String^ message)
	{
		auto dataWriter = ref new DataWriter(this->messageWebSocket->OutputStream);
		dataWriter->WriteString(message);

		Concurrency::create_task(dataWriter->StoreAsync()).then(
			[=](unsigned int)
		{
			dataWriter->DetachStream();
			std::wstringstream wstringstream;
			wstringstream << L"Sending message using MessageWebSocket: " << message->Data() << std::endl;
			::OutputDebugString(wstringstream.str().c_str());
		});
	}

	void WebSocket_MessageReceived(Windows::Networking::Sockets::MessageWebSocket^ sender, Windows::Networking::Sockets::MessageWebSocketMessageReceivedEventArgs^ args)
	{
		try
		{
			DataReader^ dataReader = args->GetDataReader();

			dataReader->UnicodeEncoding = Windows::Storage::Streams::UnicodeEncoding::Utf8;
			Platform::String^ message = dataReader->ReadString(dataReader->UnconsumedBufferLength);
			std::wstringstream wstringstream;
			wstringstream << L"Message received from MessageWebSocket: " << message->Data() << std::endl;
			::OutputDebugString(wstringstream.str().c_str());
			this->messageWebSocket->Close(1000, L"");
		}
		catch (Platform::Exception^ ex)
		{
			Windows::Web::WebErrorStatus webErrorStatus = Windows::Networking::Sockets::WebSocketError::GetStatus(ex->HResult);
			// Add additional code here to handle exceptions.
		}
	}

	void WebSocket_Closed(Windows::Networking::Sockets::IWebSocket^ sender, Windows::Networking::Sockets::WebSocketClosedEventArgs^ args)
	{
		std::wstringstream wstringstream;
		wstringstream << L"WebSocket_Closed; Code: " << args->Code << ", Reason: \"" << args->Reason->Data() << "\"" << std::endl;
		::OutputDebugString(wstringstream.str().c_str());
		// Add additional code here to handle the WebSocket being closed.
	}
```

### Handle the MessageWebSocket.MessageReceived and MessageWebSocket.Closed events

As shown in the example above, before establishing a connection and sending data with a **MessageWebSocket**, you should subscribe to the [**MessageWebSocket.MessageReceived**](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_MessageReceived) and  [**MessageWebSocket.Closed**](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_Closed) events.
 
**MessageReceived** is raised when data is received. The data can be accessed via [**MessageWebSocketMessageReceivedEventArgs**](/uwp/api/windows.networking.sockets.messagewebsocketmessagereceivedeventargs?branch=live). **Closed** is raised when the client or the server closes the socket.
 
### Send data on a MessageWebSocket

Once a connection is established, you can send data to the server. You do this by using the [**MessageWebSocket.OutputStream**](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.Sockets.MessageWebSocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_OutputStream) property, and a [**DataWriter**](/uwp/api/windows.storage.streams.datawriter?branch=live), to write the data. 

**Note** The **DataWriter** takes ownership of the output stream. When the **DataWriter** goes out of scope, if the output stream is attached to it, the **DataWriter** deallocates the output stream. After that, subsequent attempts to use the output stream fail with an HRESULT value of 0x80000013. But you can call [**DataWriter.DetachStream**](/uwp/api/windows.storage.streams.datawriter#Windows_Storage_Streams_DataWriter_DetachStream) to detach the output stream from the **DataWriter** and return ownership of the stream to the **MessageWebSocket**.

## Use StreamWebSocket to connect

[**StreamWebSocket**](/uwp/api/windows.networking.sockets.streamwebsocket?branch=live) allows sections of a message to be read with each read operation. Consequently, it's suitable when very large files (such as photos or videos) are being transferred. The class supports only binary messages.

The example code below uses the WebSocket.org echo server&mdash;a service that echoes back to the sender any message sent to it.

```csharp
private Windows.Networking.Sockets.StreamWebSocket streamWebSocket;

protected override void OnNavigatedTo(NavigationEventArgs e)
{
	this.streamWebSocket = new Windows.Networking.Sockets.StreamWebSocket();

	this.streamWebSocket.Closed += WebSocket_Closed;

	try
	{
		Task connectTask = this.streamWebSocket.ConnectAsync(new Uri("wss://echo.websocket.org")).AsTask();

		connectTask.ContinueWith(_ =>
		{
			Task.Run(() => this.ReceiveMessageUsingStreamWebSocket());
			Task.Run(() => this.SendMessageUsingStreamWebSocket(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 }));
		});
	}
	catch (Exception ex)
	{
		Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
		// Add code here to handle exceptions.
	}
}

private async void ReceiveMessageUsingStreamWebSocket()
{
	try
	{
		using (var dataReader = new DataReader(this.streamWebSocket.InputStream))
		{
			dataReader.InputStreamOptions = InputStreamOptions.Partial;
			await dataReader.LoadAsync(256);
			byte[] message = new byte[dataReader.UnconsumedBufferLength];
			dataReader.ReadBytes(message);
			Debug.WriteLine("Data received from StreamWebSocket: " + message.Length + " bytes");
		}
		this.streamWebSocket.Dispose();
	}
	catch (Exception ex)
	{
		Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
		// Add code here to handle exceptions.
	}
}

private async void SendMessageUsingStreamWebSocket(byte[] message)
{
	try
	{
		using (var dataWriter = new DataWriter(this.streamWebSocket.OutputStream))
		{
			dataWriter.WriteBytes(message);
			await dataWriter.StoreAsync();
			dataWriter.DetachStream();
		}
		Debug.WriteLine("Sending data using StreamWebSocket: " + message.Length.ToString() + " bytes");
	}
	catch (Exception ex)
	{
		Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
		// Add code here to handle exceptions.
	}
}

private void WebSocket_Closed(Windows.Networking.Sockets.IWebSocket sender, Windows.Networking.Sockets.WebSocketClosedEventArgs args)
{
	Debug.WriteLine("WebSocket_Closed; Code: " + args.Code + ", Reason: \"" + args.Reason + "\"");
	// Add additional code here to handle the WebSocket being closed.
}
```

```cpp
#include <ppltasks.h>
#include <sstream>

	...
	
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Xaml::Navigation;

	...

private:
	Windows::Networking::Sockets::StreamWebSocket^ streamWebSocket;

protected:
	virtual void OnNavigatedTo(NavigationEventArgs^ e) override
	{
		this->streamWebSocket = ref new Windows::Networking::Sockets::StreamWebSocket();

		this->streamWebSocket->Closed += ref new TypedEventHandler<Windows::Networking::Sockets::IWebSocket^, Windows::Networking::Sockets::WebSocketClosedEventArgs^>(this, &StreamWebSocketPage::WebSocket_Closed);

		try
		{
			auto connectTask = Concurrency::create_task(this->streamWebSocket->ConnectAsync(ref new Uri(L"wss://echo.websocket.org")));

			connectTask.then(
				[=]
			{
				this->ReceiveMessageUsingStreamWebSocket();
				this->SendMessageUsingStreamWebSocket(ref new Platform::Array< byte >{ 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 });
			});
		}
		catch (Platform::Exception^ ex)
		{
			Windows::Web::WebErrorStatus webErrorStatus = Windows::Networking::Sockets::WebSocketError::GetStatus(ex->HResult);
			// Add additional code here to handle exceptions.
		}
	}

private:
	void SendMessageUsingStreamWebSocket(const Platform::Array< byte >^ message)
	{
		try
		{
			auto dataWriter = ref new DataWriter(this->streamWebSocket->OutputStream);
			dataWriter->WriteBytes(message);

			Concurrency::create_task(dataWriter->StoreAsync()).then(
				[=](Concurrency::task< unsigned int >) // task< unsigned int > instead of unsigned int in order to handle any exceptions thrown in StoreAsync().
			{
				dataWriter->DetachStream();
				std::wstringstream wstringstream;
				wstringstream << L"Sending data using StreamWebSocket: " << message->Length << L" bytes" << std::endl;
				::OutputDebugString(wstringstream.str().c_str());
			});
		}
		catch (Platform::Exception^ ex)
		{
			Windows::Web::WebErrorStatus webErrorStatus = Windows::Networking::Sockets::WebSocketError::GetStatus(ex->HResult);
			// Add additional code here to handle exceptions.
		}
	}

	void ReceiveMessageUsingStreamWebSocket()
	{
		try
		{
			DataReader^ dataReader = ref new DataReader(this->streamWebSocket->InputStream);
			dataReader->InputStreamOptions = InputStreamOptions::Partial;

			Concurrency::create_task(dataReader->LoadAsync(256)).then(
				[=](unsigned int bytesLoaded)
			{
				auto message = ref new Platform::Array< byte >(bytesLoaded);
				dataReader->ReadBytes(message);
				std::wstringstream wstringstream;
				wstringstream << L"Data received from StreamWebSocket: " << message->Length << " bytes" << std::endl;
				::OutputDebugString(wstringstream.str().c_str());
				this->streamWebSocket->Close(1000, L"");
			});
		}
		catch (Platform::Exception^ ex)
		{
			Windows::Web::WebErrorStatus webErrorStatus = Windows::Networking::Sockets::WebSocketError::GetStatus(ex->HResult);
			// Add additional code here to handle exceptions.
		}
	}

	void WebSocket_Closed(Windows::Networking::Sockets::IWebSocket^ sender, Windows::Networking::Sockets::WebSocketClosedEventArgs^ args)
	{
		std::wstringstream wstringstream;
		wstringstream << L"WebSocket_Closed; Code: " << args->Code << ", Reason: \"" << args->Reason->Data() << "\"" << std::endl;
		::OutputDebugString(wstringstream.str().c_str());
		// Add additional code here to handle the WebSocket being closed.
	}
```

### Handle the StreamWebSocket.Closed event

Before establishing a connection and sending data with a **StreamWebSocket**, you should subscribe to the [**StreamWebSocket.Closed**](/uwp/api/windows.networking.sockets.streamwebsocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_Closed) event. **Closed** is raised when the client or the server closes the socket.
 
### Send data on a StreamWebSocket

Once a connection is established, you can send data to the server. You do this by using the [**StreamWebSocket.OutputStream**](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_OutputStream) property, and a [**DataWriter**](/uwp/api/windows.storage.streams.datawriter?branch=live), to write the data.

**Note** If you want to write more data on the same socket, then be sure to call [**DataWriter.DetachStream**](/uwp/api/windows.storage.streams.datawriter?branch=live#Windows_Storage_Streams_DataWriter_DetachStream) to detach the output stream from the **DataWriter** before the **DataWriter** goes out of scope. This returns ownership of the stream to the **MessageWebSocket**.

### Receive data on a StreamWebSocket

Use the [**StreamWebSocket.InputStream**](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_InputStream) property, and a [**DataReader**](/uwp/api/windows.storage.streams.datareader?branch=live), to read the data.

## Advanced options for MessageWebSocket and StreamWebSocket

Before establishing a connection, you can set advanced options on a socket by setting properties on either [**MessageWebSocketControl**](/uwp/api/windows.networking.sockets.messagewebsocketcontrol?branch=live) or [**StreamWebSocketControl**](/uwp/api/windows.networking.sockets.streamwebsocketcontrol?branch=live). You access an instance of those classes from the socket object itself either via its [**MessageWebSocket.Control**](/uwp/api/windows.networking.sockets.messagewebsocketcontrol?branch=live#Windows_Networking_Sockets_MessageWebSocket_Control) property or its [**StreamWebSocket.Control**](/uwp/api/windows.networking.sockets.streamwebsocketcontrol?branch=live#Windows_Networking_Sockets_StreamWebSocket_Control) property, as appropriate.

Here's an example using **StreamWebSocket**. The same pattern applies to **MessageWebSocket**.

```csharp
var streamWebSocket = new Windows.Networking.Sockets.StreamWebSocket();

// By default, the Nagle algorithm is not used. This overrides that, and causes it to be used.
streamWebSocket.Control.NoDelay = false;

await streamWebSocket.ConnectAsync(new Uri("wss://echo.websocket.org"));
```

```cpp
auto streamWebSocket = ref new Windows::Networking::Sockets::StreamWebSocket();

// By default, the Nagle algorithm is not used. This overrides that, and causes it to be used.
streamWebSocket->Control->NoDelay = false;

auto connectTask = Concurrency::create_task(streamWebSocket->ConnectAsync(ref new Uri(L"wss://echo.websocket.org")));
```

**Note** Don't try to change a control property *after* you've called **ConnectAsync**. The only exception to that rule is [MessageWebSocketControl.MessageType](/uwp/api/windows.networking.sockets.messagewebsocketcontrol?branch=live#Windows_Networking_Sockets_MessageWebSocketControl_MessageType).

## WebSocket information classes

[**MessageWebSocket**](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live) and [**StreamWebSocket**](/uwp/api/windows.networking.sockets.streamwebsocket?branch=live) each have a corresponding class that provides additional information about the object.

[**MessageWebSocketInformation**](/uwp/api/windows.networking.sockets.messagewebsocketinformation?branch=live) provides information about a **MessageWebSocket**, and you retrieve an instance of it using the [**MessageWebSocket.Information**](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_Information) property.

[**StreamWebSocketInformation**](/uwp/api/Windows.Networking.Sockets.StreamWebSocketInformation?branch=live) provides information about a **StreamWebSocket**, and you retrieve an instance of it using the [**StreamWebSocket.Information**](/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_Information) property.

Note that the properties on these information classes are read-only, but you can use them to retrieve information at any time during the lifetime of a web socket object.

## Handling exceptions

An error encountered on a [**MessageWebSocket**](/uwp/api/Windows.Networking.Sockets.MessageWebSocket?branch=live) or [**StreamWebSocket**](/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live) operation is returned as an **HRESULT** value. You can pass that **HRESULT** value to the [**WebSocketError.GetStatus**](/uwp/api/Windows.Networking.Sockets.WebSocketError?branch=live#Windows_Networking_Sockets_WebSocketError_GetStatus_System_Int32_) method to convert it into a [**WebErrorStatus**](/uwp/api/Windows.Web.WebErrorStatus) enumeration value.

Most **WebErrorStatus** enumeration values correspond to an error returned by the native HTTP client operation. Your app can switch on **WebErrorStatus** enumeration values to modify app behavior depending on the cause of the exception.

For parameter validation errors, you can use the **HRESULT** from the exception to learn more detailed information about the error. Possible **HRESULT** values are listed in `Winerror.h`, which can be found in your SDK installation (for example, in the folder `C:\Program Files (x86)\Windows Kits\10\Include\<VERSION>\shared`). For most parameter validation errors, the **HRESULT** returned is **E_INVALIDARG**.

## Setting timeouts on WebSocket operations

**MessageWebSocket** and **StreamWebSocket** use an internal system service to send WebSocket client requests, and to receive responses from a server. The default timeout value used for a WebSocket connect operation is 60 seconds. If the HTTP server that supports WebSockets doesn't or can't respond to the WebSocket connection request (it's temporarily down, or blocked by a network outage), then the internal system service waits the default 60 seconds before it returns an error. That error causes an exception to be thrown on the WebSocket **ConnectAsync** method. For send and receive operations after a WebSocket connection has been established, the default timeout is 30 seconds.

If the name query for an HTTP server name in the URI returns multiple IP addresses for the name, then the internal system service tries up to 5 IP addresses for the site (each with a default timeout of 60 seconds) before it fails. Consequently, your app could wait several minutes trying to connect to multiple IP addresses before it handles an exception. This behavior might appear to the user like the app has stopped working. 

To make your app more responsive and minimize these issues, you can set a shorter timeout on connection requests. You set a timeout in a similar way for both **MessageWebSocket** and **StreamWebSocket**.

```csharp
private Windows.Networking.Sockets.MessageWebSocket messageWebSocket;

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    this.messageWebSocket = new Windows.Networking.Sockets.MessageWebSocket();

    try
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var connectTask = this.messageWebSocket.ConnectAsync(new Uri("wss://echo.websocket.org")).AsTask(cancellationTokenSource.Token);

        // Cancel connectTask after 5 seconds.
        cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(5000));

        connectTask.ContinueWith((antecedent) =>
        {
            if (antecedent.Status == TaskStatus.RanToCompletion)
            {
                // connectTask ran to completion, so we know that the MessageWebSocket is connected.
                // Add additional code here to use the MessageWebSocket.
            }
            else
            {
                // connectTask timed out, or faulted.
            }
        });
    }
    catch (Exception ex)
    {
        Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
        // Add additional code here to handle exceptions.
    }
}
```

```cpp
#include <agents.h>
#include <ppltasks.h>
#include <sstream>

	...
	
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Xaml::Navigation;

	...

private:
	Windows::Networking::Sockets::MessageWebSocket^ messageWebSocket;

protected:
	virtual void OnNavigatedTo(NavigationEventArgs^ e) override
	{
		this->messageWebSocket = ref new Windows::Networking::Sockets::MessageWebSocket();

		try
		{
			Concurrency::cancellation_token_source cancellationTokenSource;
			Concurrency::cancellation_token cancellationToken = cancellationTokenSource.get_token();

			auto connectTask = Concurrency::create_task(this->messageWebSocket->ConnectAsync(ref new Uri(L"wss://echo.websocket.org")), cancellationToken);

			// This continuation task returns true should connectTask run to completion.
			Concurrency::task< bool > taskRanToCompletion = connectTask.then([](void)
			{
				return true;
			});

			// This task returns false after the specified timeout. 5 seconds, in this example.
			Concurrency::task< bool > taskTimedout = Concurrency::create_task([]() -> bool
			{
				Concurrency::task_completion_event< void > taskCompletionEvent;

				// A call object that sets the task completion event.
				auto call = std::make_shared< Concurrency::call< int > >([taskCompletionEvent](int)
				{
					taskCompletionEvent.set();
				});

				// A non-repeating timer that calls the call object when the timer fires.
				auto nonRepeatingTimer = std::make_shared< Concurrency::timer < int > >(5000, 0, call.get(), false);
				nonRepeatingTimer->start();

				// A task that completes after the completion event is set.
				Concurrency::task< void > taskWaitForCompletionEvent(taskCompletionEvent);

				return taskWaitForCompletionEvent.then([]() {return false; }).get();
			});

			(taskRanToCompletion || taskTimedout).then([this, cancellationTokenSource](bool connectTaskRanToCompletion)
			{
				if (connectTaskRanToCompletion)
				{
					// connectTask ran to completion, so we know that the MessageWebSocket is connected.
					// Add additional code here to use the MessageWebSocket.
				}
				else
				{
					// taskTimedout ran to completion, so we should cancel connectTask via the cancellation_token_source.
					cancellationTokenSource.cancel();
				}
			});
		}
		catch (Platform::Exception^ ex)
		{
			Windows::Web::WebErrorStatus webErrorStatus = Windows::Networking::Sockets::WebSocketError::GetStatus(ex->HResult);
			// Add additional code here to handle exceptions.
		}
	}
```

## Important APIs

* [DataReader](/uwp/api/Windows.Storage.Streams.DataReader?branch=live)
* [DataWriter](/uwp/api/Windows.Storage.Streams.DataWriter?branch=live)
* [DataWriter.DetachStream](/uwp/api/windows.storage.streams.datawriter?branch=live#Windows_Storage_Streams_DataWriter_DetachStream)
* [MessageWebSocket](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live)
* [MessageWebSocket.Closed](/uwp/api/Windows.Networking.Sockets.MessageWebSocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_Closed)
* [MessageWebSocket.ConnectAsync](/uwp/api/windows.networking.sockets.messagewebsocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_ConnectAsync_Windows_Foundation_Uri_)
* [MessageWebSocket.Control](/uwp/api/Windows.Networking.Sockets.MessageWebSocketControl?branch=live#Windows_Networking_Sockets_MessageWebSocket_Control)
* [MessageWebSocket.Information](/uwp/api/Windows.Networking.Sockets.MessageWebSocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_Information)
* [MessageWebSocket.MessageReceived](/uwp/api/Windows.Networking.Sockets.MessageWebSocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_MessageReceived)
* [MessageWebSocket.OutputStream](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.Sockets.MessageWebSocket?branch=live#Windows_Networking_Sockets_MessageWebSocket_OutputStream)
* [MessageWebSocketControl](/uwp/api/Windows.Networking.Sockets.MessageWebSocketControl?branch=live)
* [MessageWebSocketControl.MessageType](/uwp/api/Windows.Networking.Sockets.MessageWebSocketControl?branch=live#Windows_Networking_Sockets_MessageWebSocketControl_MessageType)
* [MessageWebSocketInformation](/uwp/api/Windows.Networking.Sockets.MessageWebSocketInformation?branch=live)
* [MessageWebSocketMessageReceivedEventArgs](/uwp/api/Windows.Networking.Sockets.MessageWebSocketMessageReceivedEventArgs?branch=live)
* [SocketMessageType](/uwp/api/windows.networking.sockets.socketmessagetype?branch=live)
* [StreamWebSocket](/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live)
* [StreamWebSocket.Closed](/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_Closed)
* [StreamSocket.ConnectAsync](/uwp/api/windows.networking.sockets.streamsocket?branch=live#Windows_Networking_Sockets_StreamSocket_ConnectAsync_Windows_Networking_EndpointPair_)
* [StreamWebSocket.Control](/uwp/api/windows.networking.sockets.streamwebsocketcontrol?branch=live#Windows_Networking_Sockets_StreamWebSocket_Control)
* [StreamWebSocket.Information](/uwp/api/windows.networking.sockets.streamwebsocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_Information)
* [StreamWebSocket.InputStream](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_InputStream)
* [StreamWebSocket.OutputStream](https://docs.microsoft.com/en-us/uwp/api/Windows.Networking.Sockets.StreamWebSocket?branch=live#Windows_Networking_Sockets_StreamWebSocket_OutputStream)
* [StreamWebSocketControl](/uwp/api/Windows.Networking.Sockets.StreamWebSocketControl?branch=live)
* [StreamWebSocketInformation](/uwp/api/Windows.Networking.Sockets.StreamWebSocketInformation?branch=live)
* [WebErrorStatus](/uwp/api/Windows.Web.WebErrorStatus?branch=live) 
* [WebSocketError.GetStatus](/uwp/api/Windows.Networking.Sockets.WebSocketError?branch=live#Windows_Networking_Sockets_WebSocketError_GetStatus_System_Int32_)
* [Windows.Networking.Sockets](/uwp/api/Windows.Networking.Sockets?branch=live)

## Related topics

* [WebSocket Protocol](http://tools.ietf.org/html/rfc6455)
* [Sockets](sockets.md)

## Samples

* [WebSocket sample](http://go.microsoft.com/fwlink/p/?LinkId=620623)
