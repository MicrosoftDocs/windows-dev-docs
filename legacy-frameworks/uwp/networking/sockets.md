---
description: Sockets are a low-level data transfer technology on top of which many networking protocols are implemented. UWP offers TCP and UDP socket classes for client-server or peer-to-peer applications, whether connections are long-lived or an established connection is not required.
title: Sockets
ms.assetid: 23B10A3C-E33F-4CD6-92CB-0FFB491472D6
ms.date: 06/03/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Sockets
Sockets are a low-level data transfer technology on top of which many networking protocols are implemented. UWP offers TCP and UDP socket classes for client-server or peer-to-peer applications, whether connections are long-lived or an established connection is not required.

This topic focuses on how to use the Universal Windows Platform (UWP) socket classes that are in the [**Windows.Networking.Sockets**](/uwp/api/Windows.Networking.Sockets) namespace. But you can also use [Windows Sockets 2 (Winsock)](/windows/desktop/WinSock/windows-sockets-start-page-2) in a UWP app.

> [!NOTE]
> As a consequence of [network isolation](/previous-versions/windows/apps/hh770532(v=win.10)), Windows disallows establishing a socket connection (Sockets or WinSock) between two UWP apps running on the same machine; whether that's via the local loopback address (127.0.0.0), or by explicitly specifying the local IP address. For details about mechanisms by which UWP apps can communicate with one another, see [App-to-app communication](../app-to-app/index.md).

## Build a basic TCP socket client and server
A TCP (Transmission Control Protocol) socket provides low-level network data transfers in either direction for connections that are long-lived. TCP sockets are the underlying feature used by most of the network protocols used on the Internet. To demonstrate basic TCP operations, the example code below shows a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) and a [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener) sending and receiving data over TCP to form an echo client and server.

To begin with as few moving parts as possible&mdash;and to sidestep network isolation issues for the present&mdash;create a new project, and put both the client and the server code below into the same project.

You'll need to [declare an app capability](../packaging/app-capability-declarations.md) in your project. Open your app package manifest source file (the `Package.appxmanifest` file) and, on the Capabilities tab, check **Private Networks (Client & Server)**. This is how that looks in the `Package.appxmanifest` markup.

```xml
<Capability Name="privateNetworkClientServer" />
```

Instead of `privateNetworkClientServer`, you can declare `internetClientServer` if you're connecting over the internet. Both **StreamSocket** and **StreamSocketListener** need one or other of these app capabilities to be declared.

### An echo client and server, using TCP sockets
Construct a [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener) and begin listening for incoming TCP connections. The [**StreamSocketListener.ConnectionReceived**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener.ConnectionReceived) event is raised each time a client establishes a connection with the **StreamSocketListener**.

Also construct a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket), establish a connection to the server, send a request, and receive a response.

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

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add(string.Format("server received the request: \"{0}\"", request)));

    // Echo the request back as the response.
    using (Stream outputStream = args.Socket.OutputStream.AsStreamForWrite())
    {
        using (var streamWriter = new StreamWriter(outputStream))
        {
            await streamWriter.WriteLineAsync(request);
            await streamWriter.FlushAsync();
        }
    }

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add(string.Format("server sent back the response: \"{0}\"", request)));

    sender.Dispose();

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add("server closed its socket"));
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

```cppwinrt
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Networking.Sockets.h>
#include <winrt/Windows.Storage.Streams.h>
#include <winrt/Windows.UI.Core.h>
#include <winrt/Windows.UI.Xaml.Navigation.h>
#include <sstream>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml::Navigation;
...
private:
    Windows::Networking::Sockets::StreamSocketListener m_streamSocketListener;
    Windows::Networking::Sockets::StreamSocket m_streamSocket;

public:
    void OnNavigatedTo(NavigationEventArgs const& /* e */)
    {
        StartServer();
        StartClient();
    }

private:
    IAsyncAction StartServer()
    {
        try
        {
            // The ConnectionReceived event is raised when connections are received.
            m_streamSocketListener.ConnectionReceived({ this, &StreamSocketAndListenerPage::OnConnectionReceived });

            // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
            // Every protocol typically has a standard port number. For example, HTTP is typically 80, FTP is 20 and 21, etc.
            // For this example, we'll choose an arbitrary port number.
            co_await m_streamSocketListener.BindServiceNameAsync(L"1337");
            serverListBox().Items().Append(winrt::box_value(L"server is listening..."));
        }
        catch (winrt::hresult_error const& ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus{ Windows::Networking::Sockets::SocketError::GetStatus(ex.to_abi()) };
            serverListBox().Items().Append(webErrorStatus != Windows::Networking::Sockets::SocketErrorStatus::Unknown ? winrt::box_value(winrt::to_hstring((int32_t)webErrorStatus)) : winrt::box_value(winrt::to_hstring(ex.to_abi())));
        }
    }

    IAsyncAction OnConnectionReceived(Windows::Networking::Sockets::StreamSocketListener /* sender */, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs args)
    {
        try
        {
            auto socket{ args.Socket() }; // Keep the socket referenced, and alive.
            DataReader dataReader{ socket.InputStream() };

            unsigned int bytesLoaded = co_await dataReader.LoadAsync(sizeof(unsigned int));

            unsigned int stringLength = dataReader.ReadUInt32();
            bytesLoaded = co_await dataReader.LoadAsync(stringLength);
            winrt::hstring request = dataReader.ReadString(bytesLoaded);

            serverListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
            {
                std::wstringstream wstringstream;
                wstringstream << L"server received the request: \"" << request.c_str() << L"\"";
                serverListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));
            });

            // Echo the request back as the response.
            DataWriter dataWriter{ socket.OutputStream() };
            dataWriter.WriteUInt32(request.size());
            dataWriter.WriteString(request);
            co_await dataWriter.StoreAsync();
            dataWriter.DetachStream();

            serverListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
            {
                std::wstringstream wstringstream;
                wstringstream << L"server sent back the response: \"" << request.c_str() << L"\"";
                serverListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));
            });

            m_streamSocketListener = nullptr;

            serverListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
            {
                serverListBox().Items().Append(winrt::box_value(L"server closed its socket"));
            });
        }
        catch (winrt::hresult_error const& ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus{ Windows::Networking::Sockets::SocketError::GetStatus(ex.to_abi()) };
            serverListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
            {
                serverListBox().Items().Append(webErrorStatus != Windows::Networking::Sockets::SocketErrorStatus::Unknown ? winrt::box_value(winrt::to_hstring((int32_t)webErrorStatus)) : winrt::box_value(winrt::to_hstring(ex.to_abi())));
            });
        }
    }

    IAsyncAction StartClient()
    {
        try
        {
            // Establish a connection to the echo server.

            // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
            Windows::Networking::HostName hostName{ L"localhost" };

            clientListBox().Items().Append(winrt::box_value(L"client is trying to connect..."));

            co_await m_streamSocket.ConnectAsync(hostName, L"1337");
            clientListBox().Items().Append(winrt::box_value(L"client connected"));

            // Send a request to the echo server.
            DataWriter dataWriter{ m_streamSocket.OutputStream() };
            winrt::hstring request{ L"Hello, World!" };
            dataWriter.WriteUInt32(request.size());
            dataWriter.WriteString(request);

            co_await dataWriter.StoreAsync();

            std::wstringstream wstringstream;
            wstringstream << L"client sent the request: \"" << request.c_str() << L"\"";
            clientListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));

            co_await dataWriter.FlushAsync();
            dataWriter.DetachStream();

            // Read data from the echo server.
            DataReader dataReader{ m_streamSocket.InputStream() };
            unsigned int bytesLoaded = co_await dataReader.LoadAsync(sizeof(unsigned int));
            unsigned int stringLength = dataReader.ReadUInt32();
            bytesLoaded = co_await dataReader.LoadAsync(stringLength);
            winrt::hstring response{ dataReader.ReadString(bytesLoaded) };

            wstringstream.str(L"");
            wstringstream << L"client received the response: \"" << response.c_str() << L"\"";
            clientListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));

            m_streamSocket = nullptr;

            clientListBox().Items().Append(winrt::box_value(L"client closed its socket"));
        }
        catch (winrt::hresult_error const& ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus{ Windows::Networking::Sockets::SocketError::GetStatus(ex.to_abi()) };
            serverListBox().Items().Append(webErrorStatus != Windows::Networking::Sockets::SocketErrorStatus::Unknown ? winrt::box_value(winrt::to_hstring((int32_t)webErrorStatus)) : winrt::box_value(winrt::to_hstring(ex.to_abi())));
        }
    }
```

```cpp
#include <ppltasks.h>
#include <sstream>
...
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml::Navigation;
...
private:
    Windows::Networking::Sockets::StreamSocketListener^ streamSocketListener;
    Windows::Networking::Sockets::StreamSocket^ streamSocket;

protected:
    virtual void OnNavigatedTo(NavigationEventArgs^ e) override
    {
        this->StartServer();
        this->StartClient();
    }

private:
    void StartServer()
    {
        try
        {
            this->streamSocketListener = ref new Windows::Networking::Sockets::StreamSocketListener();

            // The ConnectionReceived event is raised when connections are received.
            streamSocketListener->ConnectionReceived += ref new TypedEventHandler<Windows::Networking::Sockets::StreamSocketListener^, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs^>(this, &StreamSocketAndListenerPage::StreamSocketListener_ConnectionReceived);

            // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
            // Every protocol typically has a standard port number. For example, HTTP is typically 80, FTP is 20 and 21, etc.
            // For this example, we'll choose an arbitrary port number.
            Concurrency::create_task(streamSocketListener->BindServiceNameAsync(L"1337")).then(
                [=]
            {
                this->serverListBox->Items->Append(L"server is listening...");
            });
        }
        catch (Platform::Exception^ ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus = Windows::Networking::Sockets::SocketError::GetStatus(ex->HResult);
            this->serverListBox->Items->Append(webErrorStatus.ToString() != L"Unknown" ? webErrorStatus.ToString() : ex->Message);
        }
    }

    void StreamSocketListener_ConnectionReceived(Windows::Networking::Sockets::StreamSocketListener^ sender, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs^ args)
    {
        try
        {
            auto dataReader = ref new DataReader(args->Socket->InputStream);

            Concurrency::create_task(dataReader->LoadAsync(sizeof(unsigned int))).then(
                [=](unsigned int bytesLoaded)
            {
                unsigned int stringLength = dataReader->ReadUInt32();
                Concurrency::create_task(dataReader->LoadAsync(stringLength)).then(
                    [=](unsigned int bytesLoaded)
                {
                    Platform::String^ request = dataReader->ReadString(bytesLoaded);
                    this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler(
                        [=]
                    {
                        std::wstringstream wstringstream;
                        wstringstream << L"server received the request: \"" << request->Data() << L"\"";
                        this->serverListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));
                    }));

                    // Echo the request back as the response.
                    auto dataWriter = ref new DataWriter(args->Socket->OutputStream);
                    dataWriter->WriteUInt32(request->Length());
                    dataWriter->WriteString(request);
                    Concurrency::create_task(dataWriter->StoreAsync()).then(
                        [=](unsigned int)
                    {
                        dataWriter->DetachStream();

                        this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler(
                            [=]()
                        {
                            std::wstringstream wstringstream;
                            wstringstream << L"server sent back the response: \"" << request->Data() << L"\"";
                            this->serverListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));
                        }));

                        delete this->streamSocketListener;
                        this->streamSocketListener = nullptr;

                        this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler([=]() {this->serverListBox->Items->Append(L"server closed its socket"); }));
                    });
                });
            });
        }
        catch (Platform::Exception^ ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus = Windows::Networking::Sockets::SocketError::GetStatus(ex->HResult);
            this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler([=]() {this->serverListBox->Items->Append(webErrorStatus.ToString() != L"Unknown" ? webErrorStatus.ToString() : ex->Message); }));
        }
    }

    void StartClient()
    {
        try
        {
            // Create the StreamSocket and establish a connection to the echo server.
            this->streamSocket = ref new Windows::Networking::Sockets::StreamSocket();

            // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
            auto hostName = ref new Windows::Networking::HostName(L"localhost");

            this->clientListBox->Items->Append(L"client is trying to connect...");

            Concurrency::create_task(this->streamSocket->ConnectAsync(hostName, L"1337")).then(
                [=](Concurrency::task< void >)
            {
                this->clientListBox->Items->Append(L"client connected");

                // Send a request to the echo server.
                auto dataWriter = ref new DataWriter(this->streamSocket->OutputStream);
                auto request = ref new Platform::String(L"Hello, World!");
                dataWriter->WriteUInt32(request->Length());
                dataWriter->WriteString(request);

                Concurrency::create_task(dataWriter->StoreAsync()).then(
                    [=](Concurrency::task< unsigned int >)
                {
                    std::wstringstream wstringstream;
                    wstringstream << L"client sent the request: \"" << request->Data() << L"\"";
                    this->clientListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));

                    Concurrency::create_task(dataWriter->FlushAsync()).then(
                        [=](Concurrency::task< bool >)
                    {
                        dataWriter->DetachStream();

                        // Read data from the echo server.
                        auto dataReader = ref new DataReader(this->streamSocket->InputStream);
                        Concurrency::create_task(dataReader->LoadAsync(sizeof(unsigned int))).then(
                            [=](unsigned int bytesLoaded)
                        {
                            unsigned int stringLength = dataReader->ReadUInt32();
                            Concurrency::create_task(dataReader->LoadAsync(stringLength)).then(
                                [=](unsigned int bytesLoaded)
                            {
                                Platform::String^ response = dataReader->ReadString(bytesLoaded);
                                this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler(
                                    [=]
                                {
                                    std::wstringstream wstringstream;
                                    wstringstream << L"client received the response: \"" << response->Data() << L"\"";
                                    this->clientListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));

                                    delete this->streamSocket;
                                    this->streamSocket = nullptr;

                                    this->clientListBox->Items->Append(L"client closed its socket");
                                }));
                            });
                        });
                    });
                });
            });
        }
        catch (Platform::Exception^ ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus = Windows::Networking::Sockets::SocketError::GetStatus(ex->HResult);
            this->serverListBox->Items->Append(webErrorStatus.ToString() != L"Unknown" ? webErrorStatus.ToString() : ex->Message);
        }
    }
```

## References to StreamSockets in C++ PPL continuations (applies to C++/CX, primarily)
> [!NOTE]
> If you use C++/WinRT coroutines, and you pass parameters by value, then this issue doesn't apply. For parameter-passing recommendations, see [Concurrency and asynchronous operations with C++/WinRT](../cpp-and-winrt-apis/concurrency.md#parameter-passing).

A [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket?branch=live) remains alive as long as there's an active read/write on its input/output stream (let's take for example the [**StreamSocketListenerConnectionReceivedEventArgs.Socket**](/uwp/api/windows.networking.sockets.streamsocketlistenerconnectionreceivedeventargs.Socket) that you have access to in your [**StreamSocketListener.ConnectionReceived**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener.ConnectionReceived) event handler). When you call [**DataReader.LoadAsync**](/uwp/api/windows.storage.streams.datareader.loadasync) (or `ReadAsync/WriteAsync/StoreAsync`), then that holds a reference to the socket (via the socket's input stream) until the **Completed** event handler (if any) of the **LoadAsync** is done executing.

The Parallel Patterns Library (PPL) doesn't schedule task continuations inline by default. In other words, adding a continuation task (with `task::then()`) doesn't guarantee that the continuation task will execute inline as the completion handler.

```cpp
void StreamSocketListener_ConnectionReceived(Windows::Networking::Sockets::StreamSocketListener^ sender, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs^ args)
{
    auto dataReader = ref new DataReader(args->Socket->InputStream);
    Concurrency::create_task(dataReader->LoadAsync(sizeof(unsigned int))).then(
        [=](unsigned int bytesLoaded)
    {
        // Work in here isn't guaranteed to execute inline as the completion handler of the LoadAsync.
    });
}
```

From the perspective of the **StreamSocket**, the completion handler is done executing (and the socket is eligible for disposal) before the continuation body runs. So, to keep your socket from being disposed if you want to use it inside that continuation, you need to either reference the socket directly (via lambda capture) and use it, or indirectly (by continuing to access `args->Socket` inside continuations), or force continuation tasks to be inline. You can see the first technique (lambda capture) in action in the [StreamSocket sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/StreamSocket). The C++/CX code in the [Build a basic TCP socket client and server](#build-a-basic-tcp-socket-client-and-server) section above uses the second technique&mdash;it echoes the request back as a response, and it accesses `args->Socket` from within one of the innermost continuations.

The third technique is appropriate when you're not echoing a response back. You use the `task_continuation_context::use_synchronous_execution()` option to force PPL to execute the continuation body inline. Here's a code example showing how to do it.

```cpp
void StreamSocketListener_ConnectionReceived(Windows::Networking::Sockets::StreamSocketListener^ sender, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs^ args)
{
    auto dataReader = ref new DataReader(args->Socket->InputStream);

    Concurrency::create_task(dataReader->LoadAsync(sizeof(unsigned int))).then(
        [=](unsigned int bytesLoaded)
    {
        unsigned int messageLength = dataReader->ReadUInt32();
        Concurrency::create_task(dataReader->LoadAsync(messageLength)).then(
            [=](unsigned int bytesLoaded)
        {
            Platform::String^ request = dataReader->ReadString(bytesLoaded);
            this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler(
                [=]
            {
                std::wstringstream wstringstream;
                wstringstream << L"server received the request: \"" << request->Data() << L"\"";
                this->serverListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));
            }));
        });
    }, Concurrency::task_continuation_context::use_synchronous_execution());
}
```

This behavior applies to all of the sockets and WebSockets classes in the [**Windows.Networking.Sockets**](/uwp/api/Windows.Networking.Sockets?branch=live) namespace. But client-side scenarios usually store sockets in member variables, so the issue is most applicable to the [**StreamSocketListener.ConnectionReceived**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener.ConnectionReceived) scenario, as illustrated above.

## Build a basic UDP socket client and server
A UDP (User Datagram Protocol) socket is similar to a TCP socket in that it also provides low-level network data transfers in either direction. But, while a TCP socket is for long-lived connections, a UDP socket is for applications where an established connection is not required. Because UDP sockets don't maintain connection on both endpoints, they're a fast and simple solution for networking between remote machines. But UDP sockets don't ensure integrity of the network packets nor even whether packets make it to the remote destination at all. So your app will need to be designed to tolerate that. Some examples of applications that use UDP sockets are local network discovery, and local chat clients.

To demonstrate basic UDP operations, the example code below shows the [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket) class being used to both send and receive data over UDP to form an echo client and server. Create a new project, and put both the client and the server code below into the same project. Just as for a TCP socket, you'll need to declare the **Private Networks (Client & Server)** app capability.

### An echo client and server, using UDP sockets
Construct a [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket) to play the role of the echo server, bind it to a specific port number, listen for an incoming UDP message, and echo it back. The [**DatagramSocket.MessageReceived**](/uwp/api/Windows.Networking.Sockets.DatagramSocket.MessageReceived) event is raised when a message is received on the socket.

Construct another **DatagramSocket** to play the role of the echo client, bind it to a specific port number, send a UDP message, and receive a response.

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

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add(string.Format("server received the request: \"{0}\"", request)));

    // Echo the request back as the response.
    using (Stream outputStream = (await sender.GetOutputStreamAsync(args.RemoteAddress, DatagramSocketPage.ClientPortNumber)).AsStreamForWrite())
    {
        using (var streamWriter = new StreamWriter(outputStream))
        {
            await streamWriter.WriteLineAsync(request);
            await streamWriter.FlushAsync();
        }
    }

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add(string.Format("server sent back the response: \"{0}\"", request)));

    sender.Dispose();

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add("server closed its socket"));
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

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.clientListBox.Items.Add(string.Format("client received the response: \"{0}\"", response)));

    sender.Dispose();

    await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.clientListBox.Items.Add("client closed its socket"));
}
```

```cppwinrt
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Networking.Sockets.h>
#include <winrt/Windows.Storage.Streams.h>
#include <winrt/Windows.UI.Core.h>
#include <winrt/Windows.UI.Xaml.Navigation.h>
#include <sstream>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml::Navigation;
...
private:
    Windows::Networking::Sockets::DatagramSocket m_clientDatagramSocket;
    Windows::Networking::Sockets::DatagramSocket m_serverDatagramSocket;

public:
    void OnNavigatedTo(NavigationEventArgs const& /* e */)
    {
        StartServer();
        StartClient();
    }

private:
    IAsyncAction StartServer()
    {
        try
        {
            // The ConnectionReceived event is raised when connections are received.
            m_serverDatagramSocket.MessageReceived({ this, &DatagramSocketPage::ServerDatagramSocket_MessageReceived });

            serverListBox().Items().Append(winrt::box_value(L"server is about to bind..."));

            // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
            co_await m_serverDatagramSocket.BindServiceNameAsync(L"1337");
            serverListBox().Items().Append(winrt::box_value(L"server is bound to port number 1337"));
        }
        catch (winrt::hresult_error const& ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus{ Windows::Networking::Sockets::SocketError::GetStatus(ex.to_abi()) };
            serverListBox().Items().Append(webErrorStatus != Windows::Networking::Sockets::SocketErrorStatus::Unknown ? winrt::box_value(winrt::to_hstring((int32_t)webErrorStatus)) : winrt::box_value(winrt::to_hstring(ex.to_abi())));
        }
    }

    IAsyncAction ServerDatagramSocket_MessageReceived(Windows::Networking::Sockets::DatagramSocket sender, Windows::Networking::Sockets::DatagramSocketMessageReceivedEventArgs args)
    {
        DataReader dataReader{ args.GetDataReader() };
        winrt::hstring request{ dataReader.ReadString(dataReader.UnconsumedBufferLength()) };

        serverListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
        {
            std::wstringstream wstringstream;
            wstringstream << L"server received the request: \"" << request.c_str() << L"\"";
            serverListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));
        });

        // Echo the request back as the response.
        IOutputStream outputStream = co_await sender.GetOutputStreamAsync(args.RemoteAddress(), L"1336");
        DataWriter dataWriter{ outputStream };
        dataWriter.WriteString(request);

        co_await dataWriter.StoreAsync();
        dataWriter.DetachStream();

        serverListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
        {
            std::wstringstream wstringstream;
            wstringstream << L"server sent back the response: \"" << request.c_str() << L"\"";
            serverListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));

            m_serverDatagramSocket = nullptr;

            serverListBox().Items().Append(winrt::box_value(L"server closed its socket"));
        });
    }

    IAsyncAction StartClient()
    {
        try
        {
            m_clientDatagramSocket.MessageReceived({ this, &DatagramSocketPage::ClientDatagramSocket_MessageReceived });

            // Establish a connection to the echo server.
            // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
            Windows::Networking::HostName hostName{ L"localhost" };

            clientListBox().Items().Append(winrt::box_value(L"client is about to bind..."));

            co_await m_clientDatagramSocket.BindServiceNameAsync(L"1336");
            clientListBox().Items().Append(winrt::box_value(L"client is bound to port number 1336"));

            // Send a request to the echo server.
            Windows::Networking::Sockets::DatagramSocket serverDatagramSocket;
            IOutputStream outputStream = co_await m_serverDatagramSocket.GetOutputStreamAsync(hostName, L"1337");

            winrt::hstring request{ L"Hello, World!" };
            DataWriter dataWriter{ outputStream };
            dataWriter.WriteString(request);
            co_await dataWriter.StoreAsync();
            dataWriter.DetachStream();

            std::wstringstream wstringstream;
            wstringstream << L"client sent the request: \"" << request.c_str() << L"\"";
            clientListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));
        }
        catch (winrt::hresult_error const& ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus{ Windows::Networking::Sockets::SocketError::GetStatus(ex.to_abi()) };
            serverListBox().Items().Append(webErrorStatus != Windows::Networking::Sockets::SocketErrorStatus::Unknown ? winrt::box_value(winrt::to_hstring((int32_t)webErrorStatus)) : winrt::box_value(winrt::to_hstring(ex.to_abi())));
        }
    }

    void ClientDatagramSocket_MessageReceived(Windows::Networking::Sockets::DatagramSocket const& /* sender */, Windows::Networking::Sockets::DatagramSocketMessageReceivedEventArgs const& args)
    {
        DataReader dataReader{ args.GetDataReader() };
        winrt::hstring response{ dataReader.ReadString(dataReader.UnconsumedBufferLength()) };
        clientListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
        {
            std::wstringstream wstringstream;
            wstringstream << L"client received the response: \"" << response.c_str() << L"\"";
            clientListBox().Items().Append(winrt::box_value(wstringstream.str().c_str()));
        });

        m_clientDatagramSocket = nullptr;

        clientListBox().Dispatcher().RunAsync(CoreDispatcherPriority::Normal, [=]()
        {
            clientListBox().Items().Append(winrt::box_value(L"client closed its socket"));
        });
    }
```

```cpp
#include <ppltasks.h>
#include <sstream>
...
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml::Navigation;
...
private:
    Windows::Networking::Sockets::DatagramSocket^ clientDatagramSocket;
    Windows::Networking::Sockets::DatagramSocket^ serverDatagramSocket;

protected:
    virtual void OnNavigatedTo(NavigationEventArgs^ e) override
    {
        this->StartServer();
        this->StartClient();
    }

private:
    void StartServer()
    {
        try
        {
            this->serverDatagramSocket = ref new Windows::Networking::Sockets::DatagramSocket();

            // The ConnectionReceived event is raised when connections are received.
            this->serverDatagramSocket->MessageReceived += ref new TypedEventHandler<Windows::Networking::Sockets::DatagramSocket^, Windows::Networking::Sockets::DatagramSocketMessageReceivedEventArgs^>(this, &DatagramSocketPage::ServerDatagramSocket_MessageReceived);

            this->serverListBox->Items->Append(L"server is about to bind...");

            // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
            Concurrency::create_task(this->serverDatagramSocket->BindServiceNameAsync("1337")).then(
                [=]
            {
                this->serverListBox->Items->Append(L"server is bound to port number 1337");
            });
        }
        catch (Platform::Exception^ ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus = Windows::Networking::Sockets::SocketError::GetStatus(ex->HResult);
            this->serverListBox->Items->Append(webErrorStatus.ToString() != L"Unknown" ? webErrorStatus.ToString() : ex->Message);
        }
    }

    void ServerDatagramSocket_MessageReceived(Windows::Networking::Sockets::DatagramSocket^ sender, Windows::Networking::Sockets::DatagramSocketMessageReceivedEventArgs^ args)
    {
        DataReader^ dataReader = args->GetDataReader();
        Platform::String^ request = dataReader->ReadString(dataReader->UnconsumedBufferLength);
        this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler(
            [=]
        {
            std::wstringstream wstringstream;
            wstringstream << L"server received the request: \"" << request->Data() << L"\"";
            this->serverListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));
        }));

        // Echo the request back as the response.
        Concurrency::create_task(sender->GetOutputStreamAsync(args->RemoteAddress, "1336")).then(
            [=](IOutputStream^ outputStream)
        {
            auto dataWriter = ref new DataWriter(outputStream);
            dataWriter->WriteString(request);
            Concurrency::create_task(dataWriter->StoreAsync()).then(
                [=](unsigned int)
            {
                dataWriter->DetachStream();

                this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler(
                    [=]()
                {
                    std::wstringstream wstringstream;
                    wstringstream << L"server sent back the response: \"" << request->Data() << L"\"";
                    this->serverListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));

                    delete this->serverDatagramSocket;
                    this->serverDatagramSocket = nullptr;

                    this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler([=]() {this->serverListBox->Items->Append(L"server closed its socket"); }));
                }));
            });
        });
    }

    void StartClient()
    {
        try
        {
            // Create the DatagramSocket and establish a connection to the echo server.
            this->clientDatagramSocket = ref new Windows::Networking::Sockets::DatagramSocket();

            this->clientDatagramSocket->MessageReceived += ref new TypedEventHandler<Windows::Networking::Sockets::DatagramSocket^, Windows::Networking::Sockets::DatagramSocketMessageReceivedEventArgs^>(this, &DatagramSocketPage::ClientDatagramSocket_MessageReceived);

            // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
            auto hostName = ref new Windows::Networking::HostName(L"localhost");

            this->clientListBox->Items->Append(L"client is about to bind...");

            Concurrency::create_task(this->clientDatagramSocket->BindServiceNameAsync("1336")).then(
                [=]
            {
                this->clientListBox->Items->Append(L"client is bound to port number 1336");
            });

            // Send a request to the echo server.
            auto serverDatagramSocket = ref new Windows::Networking::Sockets::DatagramSocket();
            Concurrency::create_task(serverDatagramSocket->GetOutputStreamAsync(hostName, "1337")).then(
                [=](IOutputStream^ outputStream)
            {
                auto request = ref new Platform::String(L"Hello, World!");
                auto dataWriter = ref new DataWriter(outputStream);
                dataWriter->WriteString(request);
                Concurrency::create_task(dataWriter->StoreAsync()).then(
                    [=](unsigned int)
                {
                    dataWriter->DetachStream();
                    std::wstringstream wstringstream;
                    wstringstream << L"client sent the request: \"" << request->Data() << L"\"";
                    this->clientListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));
                });

            });
        }
        catch (Platform::Exception^ ex)
        {
            Windows::Networking::Sockets::SocketErrorStatus webErrorStatus = Windows::Networking::Sockets::SocketError::GetStatus(ex->HResult);
            this->serverListBox->Items->Append(webErrorStatus.ToString() != L"Unknown" ? webErrorStatus.ToString() : ex->Message);
        }
    }

    void ClientDatagramSocket_MessageReceived(Windows::Networking::Sockets::DatagramSocket^ sender, Windows::Networking::Sockets::DatagramSocketMessageReceivedEventArgs^ args)
    {
        DataReader^ dataReader = args->GetDataReader();
        Platform::String^ response = dataReader->ReadString(dataReader->UnconsumedBufferLength);
        this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler(
            [=]
        {
            std::wstringstream wstringstream;
            wstringstream << L"client received the response: \"" << response->Data() << L"\"";
            this->clientListBox->Items->Append(ref new Platform::String(wstringstream.str().c_str()));
        }));

        delete this->clientDatagramSocket;
        this->clientDatagramSocket = nullptr;

        this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler([=]() {this->clientListBox->Items->Append(L"client closed its socket"); }));
    }
```

## Background operations and the socket broker
You can use the socket broker, and control channel triggers, to ensure that your app properly receives connections or data on sockets while it's not in the foreground. For more info, see [Network communications in the background](network-communications-in-the-background.md).

## Batched sends
Whenever you write to the stream associated with a socket, a transition happens from user mode (your code) to kernel mode (where the network stack is). If you're writing many buffers at a time then these repeated transitions compound into substantial overhead. Batching your sends is a way to send multiple buffers of data together, and avoid that overhead. It's especially useful if your app is doing VoIP, VPN, or other tasks that involve moving a lot of data as efficiently as possible.

This section demonstrates a couple of batched sends techniques that you can use with a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) or a connected [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket).

To get a baseline, let's see how to send a large number of buffers in an inefficient way. Here's a minimal demo, using a **StreamSocket**.

```csharp
protected override async void OnNavigatedTo(NavigationEventArgs e)
{
    var streamSocketListener = new Windows.Networking.Sockets.StreamSocketListener();
    streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived;
    await streamSocketListener.BindServiceNameAsync("1337");

    var streamSocket = new Windows.Networking.Sockets.StreamSocket();
    await streamSocket.ConnectAsync(new Windows.Networking.HostName("localhost"), "1337");
    this.SendMultipleBuffersInefficiently(streamSocket, "Hello, World!");
    //this.BatchedSendsCSharpOnly(streamSocket, "Hello, World!");
    //this.BatchedSendsAnyUWPLanguage(streamSocket, "Hello, World!");
}

private async void StreamSocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender, Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
{
    using (var dataReader = new DataReader(args.Socket.InputStream))
    {
        dataReader.InputStreamOptions = InputStreamOptions.Partial;
        while (true)
        {
            await dataReader.LoadAsync(256);
            if (dataReader.UnconsumedBufferLength == 0) break;
            IBuffer requestBuffer = dataReader.ReadBuffer(dataReader.UnconsumedBufferLength);
            string request = Windows.Security.Cryptography.CryptographicBuffer.ConvertBinaryToString(Windows.Security.Cryptography.BinaryStringEncoding.Utf8, requestBuffer);
            Debug.WriteLine(string.Format("server received the request: \"{0}\"", request));
        }
    }
}

// This implementation incurs kernel transition overhead for each packet written.
private async void SendMultipleBuffersInefficiently(Windows.Networking.Sockets.StreamSocket streamSocket, string message)
{
    var packetsToSend = new List<IBuffer>();
    for (int count = 0; count < 5; ++count) { packetsToSend.Add(Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(message, Windows.Security.Cryptography.BinaryStringEncoding.Utf8)); }

    foreach (IBuffer packet in packetsToSend)
    {
        await streamSocket.OutputStream.WriteAsync(packet);
    }
}
```

```cppwinrt
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Networking.Sockets.h>
#include <winrt/Windows.Security.Cryptography.h>
#include <winrt/Windows.Storage.Streams.h>
#include <winrt/Windows.UI.Core.h>
#include <winrt/Windows.UI.Xaml.Navigation.h>
#include <sstream>

using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml::Navigation;
...
private:
    Windows::Networking::Sockets::StreamSocketListener m_streamSocketListener;
    Windows::Networking::Sockets::StreamSocket m_streamSocket;

public:
    IAsyncAction OnNavigatedTo(NavigationEventArgs /* e */)
    {
        m_streamSocketListener.ConnectionReceived({ this, &BatchedSendsPage::OnConnectionReceived });
        co_await m_streamSocketListener.BindServiceNameAsync(L"1337");

        co_await m_streamSocket.ConnectAsync(Windows::Networking::HostName{ L"localhost" }, L"1337");
        SendMultipleBuffersInefficientlyAsync(L"Hello, World!");
        //BatchedSendsAnyUWPLanguageAsync(L"Hello, World!");
    }

private:
    IAsyncAction OnConnectionReceived(Windows::Networking::Sockets::StreamSocketListener const& /* sender */, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs const& args)
    {
        DataReader dataReader{ args.Socket().InputStream() };
        dataReader.InputStreamOptions(Windows::Storage::Streams::InputStreamOptions::Partial);

        while (true)
        {
            unsigned int bytesLoaded = co_await dataReader.LoadAsync(256);
            if (bytesLoaded == 0) break;
            winrt::hstring message{ dataReader.ReadString(bytesLoaded) };
            ::OutputDebugString(message.c_str());
        }
    }

    // This implementation incurs kernel transition overhead for each packet written.
    IAsyncAction SendMultipleBuffersInefficientlyAsync(winrt::hstring message)
    {
        co_await winrt::resume_background();

        std::vector< IBuffer > packetsToSend;
        for (unsigned int count = 0; count < 5; ++count)
        {
            packetsToSend.push_back(Windows::Security::Cryptography::CryptographicBuffer::ConvertStringToBinary(message, Windows::Security::Cryptography::BinaryStringEncoding::Utf8));
        }

        for (auto const& element : packetsToSend)
        {
            m_streamSocket.OutputStream().WriteAsync(element).get();
        }
    }
```

```cpp
#include <ppltasks.h>
#include <sstream>
...
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml::Navigation;
...
private:
    Windows::Networking::Sockets::StreamSocketListener^ streamSocketListener;
    Windows::Networking::Sockets::StreamSocket^ streamSocket;

protected:
    virtual void OnNavigatedTo(NavigationEventArgs^ e) override
    {
        this->streamSocketListener = ref new Windows::Networking::Sockets::StreamSocketListener();
        streamSocketListener->ConnectionReceived += ref new TypedEventHandler<Windows::Networking::Sockets::StreamSocketListener^, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs^>(this, &BatchedSendsPage::StreamSocketListener_ConnectionReceived);
        Concurrency::create_task(this->streamSocketListener->BindServiceNameAsync(L"1337")).then(
            [=]
        {
            this->streamSocket = ref new Windows::Networking::Sockets::StreamSocket();
            Concurrency::create_task(this->streamSocket->ConnectAsync(ref new Windows::Networking::HostName(L"localhost"), L"1337")).then(
                [=](Concurrency::task< void >)
            {
                this->SendMultipleBuffersInefficiently(L"Hello, World!");
                // this->BatchedSendsAnyUWPLanguage(L"Hello, World!");
            }, Concurrency::task_continuation_context::use_synchronous_execution());
        });
    }

private:
    void StreamSocketListener_ConnectionReceived(Windows::Networking::Sockets::StreamSocketListener^ sender, Windows::Networking::Sockets::StreamSocketListenerConnectionReceivedEventArgs^ args)
    {
        auto dataReader = ref new DataReader(args->Socket->InputStream);
        dataReader->InputStreamOptions = Windows::Storage::Streams::InputStreamOptions::Partial;
        this->ReceiveStringRecurse(dataReader, args->Socket);
    }

    void ReceiveStringRecurse(DataReader^ dataReader, Windows::Networking::Sockets::StreamSocket^ streamSocket)
    {
        Concurrency::create_task(dataReader->LoadAsync(256)).then(
            [this, dataReader, streamSocket](unsigned int bytesLoaded)
        {
            if (bytesLoaded == 0) return;
            Platform::String^ message = dataReader->ReadString(bytesLoaded);
            ::OutputDebugString(message->Data());
            this->ReceiveStringRecurse(dataReader, streamSocket);
        });
    }

    // This implementation incurs kernel transition overhead for each packet written.
    void SendMultipleBuffersInefficiently(Platform::String^ message)
    {
        std::vector< IBuffer^ > packetsToSend{};
        for (unsigned int count = 0; count < 5; ++count)
        {
            packetsToSend.push_back(Windows::Security::Cryptography::CryptographicBuffer::ConvertStringToBinary(message, Windows::Security::Cryptography::BinaryStringEncoding::Utf8));
        }

        for (auto element : packetsToSend)
        {
            Concurrency::create_task(this->streamSocket->OutputStream->WriteAsync(element)).wait();
        }
    }
```

This first example of a more efficient technique is only appropriate if you're using C#. Change `OnNavigatedTo` to call `BatchedSendsCSharpOnly` instead of `SendMultipleBuffersInefficiently` or `SendMultipleBuffersInefficientlyAsync`.

```csharp
// A C#-only technique for batched sends.
private async void BatchedSendsCSharpOnly(Windows.Networking.Sockets.StreamSocket streamSocket, string message)
{
    var packetsToSend = new List<IBuffer>();
    for (int count = 0; count < 5; ++count) { packetsToSend.Add(Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(message, Windows.Security.Cryptography.BinaryStringEncoding.Utf8)); }

    var pendingTasks = new System.Threading.Tasks.Task[packetsToSend.Count];

    for (int index = 0; index < packetsToSend.Count; ++index)
    {
        // track all pending writes as tasks, but don't wait on one before beginning the next.
        pendingTasks[index] = streamSocket.OutputStream.WriteAsync(packetsToSend[index]).AsTask();
        // Don't modify any buffer's contents until the pending writes are complete.
    }

    // Wait for all of the pending writes to complete.
    System.Threading.Tasks.Task.WaitAll(pendingTasks);
}
```

This next example is appropriate for any UWP language, not just for C#. It relies on the behavior in [**StreamSocket.OutputStream**](/uwp/api/windows.networking.sockets.streamsocket.OutputStream) and [**DatagramSocket.OutputStream**](/uwp/api/windows.networking.sockets.datagramsocket.OutputStream) that batches sends together. The technique calls [**FlushAsync**](/uwp/api/windows.storage.streams.ioutputstream.FlushAsync) on that output stream which, as of Windows 10, is guaranteed to return only after all operations on the output stream have completed.

```csharp
// An implementation of batched sends suitable for any UWP language.
private async void BatchedSendsAnyUWPLanguage(Windows.Networking.Sockets.StreamSocket streamSocket, string message)
{
    var packetsToSend = new List<IBuffer>();
    for (int count = 0; count < 5; ++count) { packetsToSend.Add(Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(message, Windows.Security.Cryptography.BinaryStringEncoding.Utf8)); }

    var pendingWrites = new IAsyncOperationWithProgress<uint, uint>[packetsToSend.Count];

    for (int index = 0; index < packetsToSend.Count; ++index)
    {
        // track all pending writes as tasks, but don't wait on one before beginning the next.
        pendingWrites[index] = streamSocket.OutputStream.WriteAsync(packetsToSend[index]);
        // Don't modify any buffer's contents until the pending writes are complete.
    }

    // Wait for all of the pending writes to complete. This step enables batched sends on the output stream.
    await streamSocket.OutputStream.FlushAsync();
}
```

```cppwinrt
// An implementation of batched sends suitable for any UWP language.
IAsyncAction BatchedSendsAnyUWPLanguageAsync(winrt::hstring message)
{
    std::vector< IBuffer > packetsToSend{};
    std::vector< IAsyncOperationWithProgress< unsigned int, unsigned int > > pendingWrites{};
    for (unsigned int count = 0; count < 5; ++count)
    {
        packetsToSend.push_back(Windows::Security::Cryptography::CryptographicBuffer::ConvertStringToBinary(message, Windows::Security::Cryptography::BinaryStringEncoding::Utf8));
    }

    for (auto const& element : packetsToSend)
    {
        // track all pending writes as tasks, but don't wait on one before beginning the next.
        pendingWrites.push_back(m_streamSocket.OutputStream().WriteAsync(element));
        // Don't modify any buffer's contents until the pending writes are complete.
    }

    // Wait for all of the pending writes to complete. This step enables batched sends on the output stream.
    co_await m_streamSocket.OutputStream().FlushAsync();
}
```

```cpp
private:
    // An implementation of batched sends suitable for any UWP language.
    void BatchedSendsAnyUWPLanguage(Platform::String^ message)
    {
        std::vector< IBuffer^ > packetsToSend{};
        std::vector< IAsyncOperationWithProgress< unsigned int, unsigned int >^ >pendingWrites{};
        for (unsigned int count = 0; count < 5; ++count)
        {
            packetsToSend.push_back(Windows::Security::Cryptography::CryptographicBuffer::ConvertStringToBinary(message, Windows::Security::Cryptography::BinaryStringEncoding::Utf8));
        }

        for (auto element : packetsToSend)
        {
            // track all pending writes as tasks, but don't wait on one before beginning the next.
            pendingWrites.push_back(this->streamSocket->OutputStream->WriteAsync(element));
            // Don't modify any buffer's contents until the pending writes are complete.
        }

        // Wait for all of the pending writes to complete. This step enables batched sends on the output stream.
        Concurrency::create_task(this->streamSocket->OutputStream->FlushAsync());
    }
```

There are some important limitations imposed by using batched sends in your code.

-   You cannot modify the contents of the **IBuffer** instances being written until the asynchronous write is complete.
-   The **FlushAsync** pattern only works on **StreamSocket.OutputStream** and **DatagramSocket.OutputStream**.
-   The **FlushAsync** pattern only works in Windows 10 and onward.
-   In other cases, use [**Task.WaitAll**](/dotnet/api/system.threading.tasks.task.waitall?view=netcore-2.0&preserve-view=true#System_Threading_Tasks_Task_WaitAll_System_Threading_Tasks_Task___) instead of the **FlushAsync** pattern.

## Port sharing for DatagramSocket
You can configure a [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket) to coexist with other Win32 or UWP multicast sockets bound to the same address/port. You do this by setting the [**DatagramSocketControl.MulticastOnly**](/uwp/api/Windows.Networking.Sockets.DatagramSocketControl.MulticastOnly) to `true` before binding or connecting the socket. You access an instance of **DatagramSocketControl** from the **DatagramSocket** object itself via its [**DatagramSocket.Control**](/uwp/api/windows.networking.sockets.datagramsocket.Control) property.

## Providing a client certificate with the StreamSocket class
[**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) supports using SSL/TLS to authenticate the server that the client app is talking to. In some cases, the client app needs to authenticate itself to the server using an SSL/TLS client certificate. You can provide a client certificate with the [**StreamSocketControl.ClientCertificate**](/uwp/api/windows.networking.sockets.streamsocketcontrol.ClientCertificate) property before binding or connecting the socket (it must be set before the SSL/TLS handshake is started). You access an instance of **StreamSocketControl** from the **StreamSocket** object itself via its [**StreamSocket.Control**](/uwp/api/windows.networking.sockets.streamsocket.Control) property. If the server requests the client certificate then Windows will respond with the client certificate that you provided.

Use an override of [**StreamSocket.ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) that takes a [**SocketProtectionLevel**](/uwp/api/windows.networking.sockets.socketprotectionlevel), as shown in this minimal code example.

> [!IMPORTANT]
> As indicated by the comment in the code examples below, your project needs to declare the sharedUserCertificates app capability for this code to work.

```csharp
// For this code to work, you need at least one certificate to be present in the user MY certificate store.
// Plugging a smartcard into a smartcard reader connected to your PC will achieve that.
// Also, your project needs to declare the sharedUserCertificates app capability.
var certificateQuery = new Windows.Security.Cryptography.Certificates.CertificateQuery();
certificateQuery.StoreName = "MY";
IReadOnlyList<Windows.Security.Cryptography.Certificates.Certificate> certificates = await Windows.Security.Cryptography.Certificates.CertificateStores.FindAllAsync(certificateQuery);
if (certificates.Count > 0)
{
    streamSocket.Control.ClientCertificate = certificates[0];
    await streamSocket.ConnectAsync(hostName, "1337", Windows.Networking.Sockets.SocketProtectionLevel.Tls12);
}
```

```cppwinrt
// For this code to work, you need at least one certificate to be present in the user MY certificate store.
// Plugging a smartcard into a smartcard reader connected to your PC will achieve that.
// Also, your project needs to declare the sharedUserCertificates app capability.
Windows::Security::Cryptography::Certificates::CertificateQuery certificateQuery;
certificateQuery.StoreName(L"MY");
IVectorView< Windows::Security::Cryptography::Certificates::Certificate > certificates = co_await Windows::Security::Cryptography::Certificates::CertificateStores::FindAllAsync(certificateQuery);

if (certificates.Size() > 0)
{
    m_streamSocket.Control().ClientCertificate(certificates.GetAt(0));
    co_await m_streamSocket.ConnectAsync(Windows::Networking::HostName{ L"localhost" }, L"1337", Windows::Networking::Sockets::SocketProtectionLevel::Tls12);
    ...
}
```

```cpp
// For this code to work, you need at least one certificate to be present in the user MY certificate store.
// Plugging a smartcard into a smartcard reader connected to your PC will achieve that.
// Also, your project needs to declare the sharedUserCertificates app capability.
auto certificateQuery = ref new Windows::Security::Cryptography::Certificates::CertificateQuery();
certificateQuery->StoreName = L"MY";
Concurrency::create_task(Windows::Security::Cryptography::Certificates::CertificateStores::FindAllAsync(certificateQuery)).then(
    [=](IVectorView< Windows::Security::Cryptography::Certificates::Certificate^ >^ certificates)
{
    if (certificates->Size > 0)
    {
        this->streamSocket->Control->ClientCertificate = certificates->GetAt(0);
        Concurrency::create_task(this->streamSocket->ConnectAsync(ref new Windows::Networking::HostName(L"localhost"), L"1337", Windows::Networking::Sockets::SocketProtectionLevel::Tls12)).then(
            [=]
        {
            ...
        });
    }
});
```

## Handling exceptions
An error encountered on a [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket), [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket), or [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener) operation is returned as an **HRESULT** value. You can pass that **HRESULT** value to the [**SocketError.GetStatus**](/uwp/api/windows.networking.sockets.socketerror.getstatus) method to convert it into a [**SocketErrorStatus**](/uwp/api/Windows.Networking.Sockets.SocketErrorStatus) enumeration value.

Most **SocketErrorStatus** enumeration values correspond to an error returned by the native Windows sockets operation. Your app can switch on **SocketErrorStatus** enumeration values to modify app behavior depending on the cause of the exception.

For parameter validation errors, you can use the **HRESULT** from the exception to learn more detailed information about the error. Possible **HRESULT** values are listed in `Winerror.h`, which can be found in your SDK installation (for example, in the folder `C:\Program Files (x86)\Windows Kits\10\Include\<VERSION>\shared`). For most parameter validation errors, the **HRESULT** returned is **E_INVALIDARG**.

The [**HostName**](/uwp/api/Windows.Networking.HostName) constructor can throw an exception if the string passed is not a valid host name. For example, it contains characters that are not allowed, which is likely if the host name is typed in to your app by the user. Construct a **HostName** inside a try/catch block. That way, if an exception is thrown, the app can notify the user and request a new host name.

## Important APIs
* [CertificateQuery](/uwp/api/windows.security.cryptography.certificates.certificatequery)
* [CertificateStores.FindAllAsync](/uwp/api/windows.security.cryptography.certificates.certificatestores.findallasync)
* [DatagramSocket](/uwp/api/Windows.Networking.Sockets.DatagramSocket)
* [DatagramSocket.BindServiceNameAsync](/uwp/api/windows.networking.sockets.datagramsocket.bindservicenameasync)
* [DatagramSocket.Control](/uwp/api/windows.networking.sockets.datagramsocket.Control)
* [DatagramSocket.GetOutputStreamAsync](/uwp/api/windows.networking.sockets.datagramsocket.getoutputstreamasync)
* [DatagramSocket.MessageReceived](/uwp/api/Windows.Networking.Sockets.DatagramSocket.MessageReceived)
* [DatagramSocketControl.MulticastOnly](/uwp/api/Windows.Networking.Sockets.DatagramSocketControl.MulticastOnly)
* [DatagramSocketMessageReceivedEventArgs](/uwp/api/windows.networking.sockets.datagramsocketmessagereceivedeventargs)
* [DatagramSocketMessageReceivedEventArgs.GetDataReader](/uwp/api/windows.networking.sockets.datagramsocketmessagereceivedeventargs.GetDataReader)
* [DataReader.LoadAsync](/uwp/api/windows.storage.streams.datareader.loadasync)
* [IOutputStream.FlushAsync](/uwp/api/windows.storage.streams.ioutputstream.FlushAsync)
* [SocketError.GetStatus](/uwp/api/windows.networking.sockets.socketerror.getstatus)
* [SocketErrorStatus](/uwp/api/Windows.Networking.Sockets.SocketErrorStatus)
* [SocketProtectionLevel](/uwp/api/windows.networking.sockets.socketprotectionlevel)
* [StreamSocket](/uwp/api/Windows.Networking.Sockets.StreamSocket)
* [StreamSocketControl.ClientCertificate](/uwp/api/windows.networking.sockets.streamsocketcontrol.ClientCertificate)
* [StreamSocket.ConnectAsync](/uwp/api/windows.networking.sockets.streamsocket.connectasync)
* [StreamSocket.InputStream](/uwp/api/windows.networking.sockets.streamsocket.InputStream)
* [StreamSocket.OutputStream](/uwp/api/windows.networking.sockets.streamsocket.OutputStream)
* [StreamSocketListener](/uwp/api/Windows.Networking.Sockets.StreamSocketListener)
* [StreamSocketListener.BindServiceNameAsync](/uwp/api/windows.networking.sockets.streamsocketlistener.bindservicenameasync)
* [StreamSocketListener.ConnectionReceived](/uwp/api/Windows.Networking.Sockets.StreamSocketListener.ConnectionReceived)
* [StreamSocketListenerConnectionReceivedEventArgs](/uwp/api/windows.networking.sockets.streamsocketlistenerconnectionreceivedeventargs)
* [Windows.Networking.Sockets](/uwp/api/Windows.Networking.Sockets)

## Related topics
* [App-to-app communication](../app-to-app/index.md)
* [Concurrency and asynchronous operations with C++/WinRT](../cpp-and-winrt-apis/concurrency.md)
* [How to set network capabilities](/previous-versions/windows/apps/hh770532(v=win.10))
* [Windows Sockets 2 (Winsock)](/windows/desktop/WinSock/windows-sockets-start-page-2)

## Samples
* [StreamSocket sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/StreamSocket)
