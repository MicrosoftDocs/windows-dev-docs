---
description: Learn how to enable your app for networking by adding network capability elements, providing authentication, and providing a secured connection.
title: Networking basics
ms.assetid: 1F47D33B-6F00-4F74-A52D-538851FD38BE
ms.date: 06/01/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Networking basics
Things you must do for any network-enabled app.

## Capabilities
In order to use networking, you must add appropriate capability elements to your app manifest. If no network capability is specified in your app's manifest, your app will have no networking capability, and any attempt to connect to the network will fail.

The following are the most-used networking capabilities.

| Capability | Description |
|------------|-------------|
| **internetClient** | Provides outbound access to the Internet and networks in public places, like airports and coffee shop. Most apps that require Internet access should use this capability. |
| **internetClientServer** | Gives the app inbound and outbound network access from the Internet and networks in public places like airports and coffee shops. |
| **privateNetworkClientServer** | Gives the app inbound and outbound network access at the user's trusted places, like home and work. |

There are other capabilities that might be necessary for your app, in certain circumstances.

| Capability | Description |
|------------|-------------|
| **enterpriseAuthentication** | Allows an app to connect to network resources that require domain credentials. For example, an app that retrieves data from SharePoint servers on a private Intranet. With this capability your credentials can be used to access network resources on a network that requires credentials. An app with this capability can impersonate you on the network. You don't need this capability in order to allow your app to access the Internet via an authenticating proxy.<br/><br/>For more details, see the documentation for the *Enterprise* capability scenario in [Restricted capabilities](../packaging/app-capability-declarations.md#restricted-capabilities). |
| **proximity** | Required for near-field proximity communication with devices in close proximity to the computer. Near-field proximity may be used to send or connect with an application on a nearby device. <br/><br/> This capability allows an app to access the network to connect to a device in close proximity, with user consent to send an invite or accept an invite. |
| **sharedUserCertificates** | This capability allows an app to access software and hardware certificates, such as smart card certificates. When this capability is invoked at runtime, the user must take action, such as inserting a card or selecting a certificate. <br/><br/> With this capability, your software and hardware certificates or a smart card are used for identification in the app. This capability may be used by your employer, bank, or government services for identification. |

## Communicating when your app is not in the foreground
[Support your app with background tasks](../launch-resume/support-your-app-with-background-tasks.md) contains general information about using background tasks to do work when your app is not in the foreground. More specifically, your code must take special steps to be notified when it is not the current foreground app and data arrives over the network for it. You used Control Channel Triggers for this purpose in Windows 8, and they are still supported in Windows 10. Full information about using Control Channel Triggers is available [**here**](/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger). A new technology in Windows 10 provides better functionality with lower overhead for some scenarios, such as push-enabled stream sockets: the socket broker and socket activity triggers.

If your app uses [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket), [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket), or [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener), then your app can transfer ownership of an open socket to a socket broker provided by the system, and then leave the foreground, or even terminate. When a connection is made on the transferred socket, or traffic arrives on that socket, then your app or its designated background task are activated. If your app is not running, it is started. The socket broker then notifies your app using a [**SocketActivityTrigger**](/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger) that new traffic has arrived. Your app reclaims the socket from the socket broker and process the traffic on the socket. This means that your app consumes far less system resources when it is not actively processing network traffic.

The socket broker is intended to replace Control Channel Triggers where it is applicable, because it provides the same functionality, but with fewer restrictions and a smaller memory footprint. Socket broker can be used by apps that are not lock screen apps, and it is used the same way on phones as on other devices. Apps need not be running when traffic arrives in order to be activated by the socket broker. And the socket broker supports listening on TCP sockets, which Control Channel Triggers do not support.

### Choosing a network trigger
There are some scenarios where either kind of trigger would be suitable. When you are choosing which kind of trigger to use in your app, consider the following advice.

-   If you are using [**IXMLHTTPRequest2**](/previous-versions/windows/desktop/api/msxml6/nn-msxml6-ixmlhttprequest2), [**System.Net.Http.HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) or [System.Net.Http.HttpClientHandler](/dotnet/api/system.net.http.httpclienthandler), you must use [**ControlChannelTrigger**](/uwp/api/Windows.Networking.Sockets.ControlChannelTrigger).
-   If you are using push-enabled **StreamSockets**, you can use control channel triggers, but should prefer [**SocketActivityTrigger**](/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger). The latter choice allows the system to free up memory and reduce power requirements when the connection is not being actively used.
-   If you want to minimize the memory footprint of your app when it is not actively servicing network requests, prefer [**SocketActivityTrigger**](/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger) when possible.
-   If you want your app to be able to receive data while the system is in Connected Standby mode, use [**SocketActivityTrigger**](/uwp/api/Windows.ApplicationModel.Background.SocketActivityTrigger).

For details and examples of how to use the socket broker, see [Network communications in the background](network-communications-in-the-background.md).

## Secured connections
Secure Sockets Layer (SSL) and the more recent Transport Layer Security (TLS) are cryptographic protocols designed to provide authentication and encryption for network communication. These protocols are designed to prevent eavesdropping and tampering when sending and receiving network data. These protocols use a client-server model for the protocol exchanges. These protocols also use digital certificates and certificate authorities to verify that the server is who it claims to be.

### Creating secure socket connections
A [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) object can be configured to use SSL/TLS for communications between the client and the server. This support for SSL/TLS is limited to using the **StreamSocket** object as the client in the SSL/TLS negotiation. You cannot use SSL/TLS with the **StreamSocket** created by a [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener) when incoming communications are received, because SSL/TLS negotiation as a server is not implemented by the **StreamSocket** class.

There are two ways to secure a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) connection with SSL/TLS:

-   [**ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) - Make the initial connection to a network service and negotiate immediately to use SSL/TLS for all communications.
-   [**UpgradeToSslAsync**](/uwp/api/windows.networking.sockets.streamsocket.upgradetosslasync) - Connect initially to a network service without encryption. The app may send or receive data. Then, upgrade the connection to use SSL/TLS for all further communications.

The SocketProtectionLevel specifies the desired socket protection level the app wants to establish or upgrade the connection with. However, the eventual protection level of the established connection is determined in a negotiation process between both endpoints of the connection. The result can be a lower protection level than the one you specified, if the other endpoint requests a lower level. 

 After the async operation has completed successfully, you can retrieve the requested protection level used in the [**ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) or [**UpgradeToSslAsync**](/uwp/api/windows.networking.sockets.streamsocket.upgradetosslasync) call through the  [**StreamSocketinformation.ProtectionLevel**](/uwp/api/windows.networking.sockets.streamsocketinformation.protectionlevel) property. However, this does not reflect the actual protection level the connection is using.

> [!NOTE]
> Your code should not implicitly depend on using a particular protection level, or on the assumption that a given security level is used by default. The security landscape changes constantly, and protocols and default protection levels change over time in order to avoid the use of protocols with known weaknesses. Defaults can vary depending on individual machine configuration, or on which software is installed and which patches have been applied. If your app depends on the use of a particular security level, then you must explicitly specify that level and then check to be sure that it is actually in use on the established connection.

### Use ConnectAsync
[**ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) can be used to establish the initial connection with a network service and then negotiate immediately to use SSL/TLS for all communications. There are two **ConnectAsync** methods that support passing a *protectionLevel* parameter:

-   [**ConnectAsync(EndpointPair, SocketProtectionLevel)**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) - Starts an asynchronous operation on a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) object to connect to a remote network destination specified as an [**EndpointPair**](/uwp/api/Windows.Networking.EndpointPair) object and a [**SocketProtectionLevel**](/uwp/api/Windows.Networking.Sockets.SocketProtectionLevel).
-   [**ConnectAsync(HostName, String, SocketProtectionLevel)**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) - Starts an asynchronous operation on a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) object to connect to a remote destination specified by a remote hostname, a remote service name, and a [**SocketProtectionLevel**](/uwp/api/Windows.Networking.Sockets.SocketProtectionLevel).

If the *protectionLevel* parameter is set to **Windows.Networking.Sockets.SocketProtectionLevel.Ssl** when calling either of the above [**ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) methods, the [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) must will be established to use SSL/TLS for encryption. This value requires encryption and never allows a NULL cipher to be used.

The normal sequence to use with one of these [**ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) methods is the same.

-   Create a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket).
-   If an advanced option on the socket is needed, use the [**StreamSocket.Control**](/uwp/api/windows.networking.sockets.streamsocket.control) property to get the [**StreamSocketControl**](/uwp/api/Windows.Networking.Sockets.StreamSocketControl) instance associated with a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) object. Set a property on the **StreamSocketControl**.
-   Call one of the above [**ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) methods to start an operation to connect to a remote destination and immediately negotiate the use of SSL/TLS.
-   The SSL strength actually negotiated using [**ConnectAsync**](/uwp/api/windows.networking.sockets.streamsocket.connectasync) can be determined by getting the [**StreamSocketinformation.ProtectionLevel**](/uwp/api/windows.networking.sockets.streamsocketinformation.protectionlevel) property after the async operation has completed successfully.

The following example creates a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) and tries to establish a connection to the network service and negotiate immediately to use SSL/TLS. If the negotiation is successful, all network communication using the **StreamSocket** between the client the network server will be encrypted.

```csharp
using Windows.Networking;
using Windows.Networking.Sockets;

    // Define some variables and set values
    StreamSocket clientSocket = new StreamSocket();
     
    HostName serverHost = new HostName("www.contoso.com");
    string serverServiceName = "https";
    
    // For simplicity, the sample omits implementation of the
    // NotifyUser method used to display status and error messages 
    
    // Try to connect to contoso using HTTPS (port 443)
    try {

        // Call ConnectAsync method with SSL
        await clientSocket.ConnectAsync(serverHost, serverServiceName, SocketProtectionLevel.Ssl);

        NotifyUser("Connected");
    }
    catch (Exception exception) {
        // If this is an unknown status it means that the error is fatal and retry will likely fail.
        if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown) {
            throw;
        }
        
        NotifyUser("Connect failed with error: " + exception.Message);
        // Could retry the connection, but for this simple example
        // just close the socket.
        
        clientSocket.Dispose();
        clientSocket = null; 
    }
           
    // Add code to send and receive data using the clientSocket
    // and then close the clientSocket
```

```cppwinrt
#include <winrt/Windows.Networking.Sockets.h>

using namespace winrt;
...
    // Define some variables, and set values.
    Windows::Networking::Sockets::StreamSocket clientSocket;

    Windows::Networking::HostName serverHost{ L"www.contoso.com" };
    winrt::hstring serverServiceName{ L"https" };

    // For simplicity, the sample omits implementation of the
    // NotifyUser method used to display status and error messages.

    // Try to connect to the server using HTTPS and SSL (port 443).
    try
    {
        co_await clientSocket.ConnectAsync(serverHost, serverServiceName, Windows::Networking::Sockets::SocketProtectionLevel::Tls12);
        NotifyUser(L"Connected");
    }
    catch (winrt::hresult_error const& exception)
    {
        NotifyUser(L"Connect failed with error: " + exception.message());
        clientSocket = nullptr;
    }
    // Add code to send and receive data using the clientSocket,
    // then set the clientSocket to nullptr when done to close it.
```

```cpp
using Windows::Networking;
using Windows::Networking::Sockets;

    // Define some variables and set values
    StreamSocket^ clientSocket = new ref StreamSocket();
 
    HostName^ serverHost = new ref HostName("www.contoso.com");
    String serverServiceName = "https";

    // For simplicity, the sample omits implementation of the
    // NotifyUser method used to display status and error messages 

    // Try to connect to the server using HTTPS and SSL (port 443)
    task<void>(clientSocket->ConnectAsync(serverHost, serverServiceName, SocketProtectionLevel::SSL)).then([this] (task<void> previousTask) {
        try
        {
            // Try getting all exceptions from the continuation chain above this point.
            previousTask.Get();
            NotifyUser("Connected");
        }
        catch (Exception^ exception)
        {
            NotifyUser("Connect failed with error: " + exception->Message);
            
            clientSocket.Close();
            clientSocket = null;
        }
    });
    // Add code to send and receive data using the clientSocket
    // Then close the clientSocket when done
```

### Use UpgradeToSslAsync
When your code uses [**UpgradeToSslAsync**](/uwp/api/windows.networking.sockets.streamsocket.upgradetosslasync), it first establishes a connection to a network service without encryption. The app may send or receive some data, then upgrade the connection to use SSL/TLS for all further communications.

The [**UpgradeToSslAsync**](/uwp/api/windows.networking.sockets.streamsocket.upgradetosslasync) method takes two parameters. The *protectionLevel* parameter indicates the protection level desired. The *validationHostName* parameter is the hostname of the remote network destination that is used for validation when upgrading to SSL. Normally the *validationHostName* would be the same hostname that the app used to initially establish the connection. If the *protectionLevel* parameter is set to **Windows.System.Socket.SocketProtectionLevel.Ssl** when calling **UpgradeToSslAsync**, the [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) must use the SSL/TLS for encryption on further communications over the socket. This value requires encryption and never allows a NULL cipher to be used.

The normal sequence to use with the [**UpgradeToSslAsync**](/uwp/api/windows.networking.sockets.streamsocket.upgradetosslasync) method is as follows:

-   Create a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket).
-   If an advanced option on the socket is needed, use the [**StreamSocket.Control**](/uwp/api/windows.networking.sockets.streamsocket.control) property to get the [**StreamSocketControl**](/uwp/api/Windows.Networking.Sockets.StreamSocketControl) instance associated with a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) object. Set a property on the **StreamSocketControl**.
-   If any data needs to be sent and received unencrypted, send it now.
-   Call the [**UpgradeToSslAsync**](/uwp/api/windows.networking.sockets.streamsocket.upgradetosslasync) method to start an operation to upgrade the connection to use SSL/TLS.
-   The SSL strength actually negotiated using [**UpgradeToSslAsync**](/uwp/api/windows.networking.sockets.streamsocket.upgradetosslasync) can be determined by getting the [**StreamSocketinformation.ProtectionLevel**](/uwp/api/windows.networking.sockets.streamsocketinformation.protectionlevel) property after the async operation completes successfully.

The following example creates a [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket), tries to establish a connection to the network service, sends some initial data, and then negotiates to use SSL/TLS. If the negotiation is successful, all network communication using the **StreamSocket** between the client and the network server will be encrypted.

```csharp
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

    // Define some variables and set values
    StreamSocket clientSocket = new StreamSocket();
 
    HostName serverHost = new HostName("www.contoso.com");
    string serverServiceName = "http";

    // For simplicity, the sample omits implementation of the
    // NotifyUser method used to display status and error messages 

    // Try to connect to contoso using HTTP (port 80)
    try {
        // Call ConnectAsync method with a plain socket
        await clientSocket.ConnectAsync(serverHost, serverServiceName, SocketProtectionLevel.PlainSocket);

        NotifyUser("Connected");

    }
    catch (Exception exception) {
        // If this is an unknown status it means that the error is fatal and retry will likely fail.
        if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown) {
            throw;
        }

        NotifyUser("Connect failed with error: " + exception.Message, NotifyType.ErrorMessage);
        // Could retry the connection, but for this simple example
        // just close the socket.

        clientSocket.Dispose();
        clientSocket = null; 
        return;
    }

    // Now try to send some data
    DataWriter writer = new DataWriter(clientSocket.OutputStream);
    string hello = "Hello, World! ☺ ";
    Int32 len = (int) writer.MeasureString(hello); // Gets the UTF-8 string length.
    writer.WriteInt32(len);
    writer.WriteString(hello);
    NotifyUser("Client: sending hello");

    try {
        // Call StoreAsync method to store the hello message
        await writer.StoreAsync();

        NotifyUser("Client: sent data");

        writer.DetachStream(); // Detach stream, if not, DataWriter destructor will close it.
    }
    catch (Exception exception) {
        NotifyUser("Store failed with error: " + exception.Message);
        // Could retry the store, but for this simple example
            // just close the socket.

            clientSocket.Dispose();
            clientSocket = null; 
            return;
    }

    // Now upgrade the client to use SSL
    try {
        // Try to upgrade to SSL
        await clientSocket.UpgradeToSslAsync(SocketProtectionLevel.Ssl, serverHost);

        NotifyUser("Client: upgrade to SSL completed");
           
        // Add code to send and receive data 
        // The close clientSocket when done
    }
    catch (Exception exception) {
        // If this is an unknown status it means that the error is fatal and retry will likely fail.
        if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown) {
            throw;
        }

        NotifyUser("Upgrade to SSL failed with error: " + exception.Message);

        clientSocket.Dispose();
        clientSocket = null; 
        return;
    }
```

```cppwinrt
#include <winrt/Windows.Networking.Sockets.h>
#include <winrt/Windows.Storage.Streams.h>

using namespace winrt;
using namespace Windows::Storage::Streams;
...
    // Define some variables, and set values.
    Windows::Networking::Sockets::StreamSocket clientSocket;

    Windows::Networking::HostName serverHost{ L"www.contoso.com" };
    winrt::hstring serverServiceName{ L"https" };

    // For simplicity, the sample omits implementation of the
    // NotifyUser method used to display status and error messages. 

    // Try to connect to the server using HTTP (port 80).
    try
    {
        co_await clientSocket.ConnectAsync(serverHost, serverServiceName, Windows::Networking::Sockets::SocketProtectionLevel::PlainSocket);
        NotifyUser(L"Connected");
    }
    catch (winrt::hresult_error const& exception)
    {
        NotifyUser(L"Connect failed with error: " + exception.message());
        clientSocket = nullptr;
    }

    // Now, try to send some data.
    DataWriter writer{ clientSocket.OutputStream() };
    winrt::hstring hello{ L"Hello, World! ☺ " };
    uint32_t len{ writer.MeasureString(hello) }; // Gets the size of the string, in bytes.
    writer.WriteInt32(len);
    writer.WriteString(hello);
    NotifyUser(L"Client: sending hello");

    try
    {
        co_await writer.StoreAsync();
        NotifyUser(L"Client: sent hello");

        writer.DetachStream(); // Detach the stream when you want to continue using it; otherwise, the DataWriter destructor closes it.
    }
    catch (winrt::hresult_error const& exception)
    {
        NotifyUser(L"Store failed with error: " + exception.message());
        // We could retry the store operation. But, for this simple example, just close the socket by setting it to nullptr.
        clientSocket = nullptr;
        co_return;
    }

    // Now, upgrade the client to use SSL.
    try
    {
        co_await clientSocket.UpgradeToSslAsync(Windows::Networking::Sockets::SocketProtectionLevel::Tls12, serverHost);
        NotifyUser(L"Client: upgrade to SSL completed");

        // Add code to send and receive data using the clientSocket,
        // then set the clientSocket to nullptr when done to close it.
    }
    catch (winrt::hresult_error const& exception)
    {
        // If this is an unknown status, then the error is fatal and retry will likely fail.
        Windows::Networking::Sockets::SocketErrorStatus socketErrorStatus{ Windows::Networking::Sockets::SocketError::GetStatus(exception.to_abi()) };
        if (socketErrorStatus == Windows::Networking::Sockets::SocketErrorStatus::Unknown)
        {
            throw;
        }

        NotifyUser(L"Upgrade to SSL failed with error: " + exception.message());
        // We could retry the store operation. But for this simple example, just close the socket by setting it to nullptr.
        clientSocket = nullptr;
        co_return;
    }
```

```cpp
using Windows::Networking;
using Windows::Networking::Sockets;
using Windows::Storage::Streams;

    // Define some variables and set values
    StreamSocket^ clientSocket = new ref StreamSocket();
 
    Hostname^ serverHost = new ref HostName("www.contoso.com");
    String serverServiceName = "http";

    // For simplicity, the sample omits implementation of the
    // NotifyUser method used to display status and error messages 

    // Try to connect to contoso using HTTP (port 80)
    task<void>(clientSocket->ConnectAsync(serverHost, serverServiceName, SocketProtectionLevel::PlainSocket)).then([this] (task<void> previousTask) {
        try
        {
            // Try getting all exceptions from the continuation chain above this point.
            previousTask.Get();
            NotifyUser("Connected");
        }
        catch (Exception^ exception)
        {
            NotifyUser("Connect failed with error: " + exception->Message);
 
            clientSocket->Close();
            clientSocket = null;
        }
    });
       
    // Now try to send some data
    DataWriter^ writer = new ref DataWriter(clientSocket.OutputStream);
    String hello = "Hello, World! ☺ ";
    Int32 len = (int) writer->MeasureString(hello); // Gets the UTF-8 string length.
    writer->writeInt32(len);
    writer->writeString(hello);
    NotifyUser("Client: sending hello");

    task<void>(writer->StoreAsync()).then([this] (task<void> previousTask) {
        try {
            // Try getting all exceptions from the continuation chain above this point.
            previousTask.Get();

            NotifyUser("Client: sent hello");

            writer->DetachStream(); // Detach stream, if not, DataWriter destructor will close it.
       }
       catch (Exception^ exception) {
               NotifyUser("Store failed with error: " + exception->Message);
               // Could retry the store, but for this simple example
               // just close the socket.
 
               clientSocket->Close();
               clientSocket = null;
               return
       }
    });

    // Now upgrade the client to use SSL
    task<void>(clientSocket->UpgradeToSslAsync(clientSocket.SocketProtectionLevel.Ssl, serverHost)).then([this] (task<void> previousTask) {
        try {
            // Try getting all exceptions from the continuation chain above this point.
            previousTask.Get();

           NotifyUser("Client: upgrade to SSL completed");
           
           // Add code to send and receive data 
           // Then close clientSocket when done
        }
        catch (Exception^ exception) {
            // If this is an unknown status it means that the error is fatal and retry will likely fail.
            if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown) {
                throw;
            }

            NotifyUser("Upgrade to SSL failed with error: " + exception.Message);

            clientSocket->Close();
            clientSocket = null; 
            return;
        }
    });
```

### Creating secure WebSocket connections
Like traditional socket connections, WebSocket connections can also be encrypted with Transport Layer Security (TLS)/Secure Sockets Layer (SSL) when using the [**StreamWebSocket**](/uwp/api/Windows.Networking.Sockets.StreamWebSocket) and [**MessageWebSocket**](/uwp/api/Windows.Networking.Sockets.MessageWebSocket) features for a UWP app. In most cases you'll want to use a secure WebSocket connection. This will increase the chances that your connection will succeed, as many proxies will reject unencrypted WebSocket connections.

For examples of how to create, or upgrade to, a secure socket connection to a network service, see [How to secure WebSocket connections with TLS/SSL](/previous-versions/windows/apps/hh994399(v=win.10)).

In addition to TLS/SSL encryption, a server may require a **Sec-WebSocket-Protocol** header value to complete the initial handshake. This value, represented by the [**StreamWebSocketInformation.Protocol**](/uwp/api/windows.networking.sockets.streamwebsocketinformation.protocol) and [**MessageWebSocketInformation.Protocol**](/uwp/api/windows.networking.sockets.messagewebsocketinformation.protocol) properties, indicate the protocol version of the connection and enables the server to correctly interpret the opening handshake and the data being exchanged afterwards. Using this protocol information, if at any point if the server cannot interpret the incoming data in a safe manner the connection can be closed.

If the initial request from the client either does not contain this value, or provides a value that doesn't match what the server expects, the expected value is sent from the server to the client on WebSocket handshake error.

## Authentication
How to provide authentication credentials when connecting over the network.

### Providing a client certificate with the StreamSocket class
The [**Windows.Networking.Sockets.StreamSocket**](/uwp/api/windows.networking.sockets.streamsocket) class supports using SSL/TLS to authenticate the server the app is talking to. In certain cases, the app also needs to authenticate itself to the server using a TLS client certificate. In Windows 10, you can provide a client certificate on the [**StreamSocket.Control**](/uwp/api/Windows.Networking.Sockets.StreamSocketControl) object (this must be set before the TLS handshake is started). If the server requests the client certificate, Windows will respond with the certificate provided.

Here is a code snippet showing how to implement this:

```csharp
var socket = new StreamSocket();
Windows.Security.Cryptography.Certificates.Certificate certificate = await GetClientCert();
socket.Control.ClientCertificate = certificate;
await socket.ConnectAsync(destination, SocketProtectionLevel.Tls12);
```

### Providing authentication credentials to a web service
The networking APIs that enable apps to interact with secure web services each provide their own methods to either initialize a client or set a request header with server and proxy authentication credentials. Each method is set with a [**PasswordCredential**](/uwp/api/Windows.Security.Credentials.PasswordCredential) object that indicates a user name, password, and the resource for which these credentials are used. The following table provides a mapping of these APIs:

| **WebSockets** | [**MessageWebSocketControl.ServerCredential**](/uwp/api/windows.networking.sockets.messagewebsocketcontrol.servercredential) |
|-------------------------|----------------------------------------------------------------------------------------------------------|
|  | [**MessageWebSocketControl.ProxyCredential**](/uwp/api/windows.networking.sockets.messagewebsocketcontrol.proxycredential) |
|  | [**StreamWebSocketControl.ServerCredential**](/uwp/api/windows.networking.sockets.streamwebsocketcontrol.servercredential) |
|  | [**StreamWebSocketControl.ProxyCredential**](/uwp/api/windows.networking.sockets.streamwebsocketcontrol.proxycredential) |
| **Background Transfer** | [**BackgroundDownloader.ServerCredential**](/uwp/api/windows.networking.backgroundtransfer.backgrounddownloader.servercredential) |
|  | [**BackgroundDownloader.ProxyCredential**](/uwp/api/windows.networking.backgroundtransfer.backgrounddownloader.proxycredential) |
|  | [**BackgroundUploader.ServerCredential**](/uwp/api/windows.networking.backgroundtransfer.backgrounduploader.servercredential) |
|  | [**BackgroundUploader.ProxyCredential**](/uwp/api/windows.networking.backgroundtransfer.backgrounduploader.proxycredential) |
| **Syndication** | [**SyndicationClient(PasswordCredential)**](/uwp/api/windows.web.syndication.syndicationclient.-ctor#Windows_Web_Syndication_SyndicationClient__ctor_Windows_Security_Credentials_PasswordCredential_) |
|  | [**SyndicationClient.ServerCredential**](/uwp/api/windows.web.syndication.syndicationclient.servercredential) |
|  | [**SyndicationClient.ProxyCredential**](/uwp/api/windows.web.syndication.syndicationclient.proxycredential) |
| **AtomPub** | [**AtomPubClient(PasswordCredential)**](/uwp/api/windows.web.atompub.atompubclient.-ctor#Windows_Web_AtomPub_AtomPubClient__ctor_Windows_Security_Credentials_PasswordCredential_) |
|  | [**AtomPubClient.ServerCredential**](/uwp/api/windows.web.atompub.atompubclient.servercredential) |
|  | [**AtomPubClient.ProxyCredential**](/uwp/api/windows.web.atompub.atompubclient.proxycredential) |

## Handling network exceptions
In most areas of programming, an exception indicates a significant problem or failure, caused by some flaw in the program. In network programming, there is an additional source for exceptions: the network itself, and the nature of network communications. Network communications are inherently unreliable and prone to unexpected failure. For each of the ways your app uses networking, you must maintain some state information; and your app code must handle network exceptions by updating that state information and initiating appropriate logic for your app to re-establish or retry communication failures.

When Universal Windows apps throw an exception, your exception handler can retrieve more detailed information on the cause of the exception to better understand the failure and make appropriate decisions.

Each language projection supports a method to access this more detailed information. An exception projects as an **HRESULT** value in Universal Windows apps. The *Winerror.h* include file contains a very large list of possible **HRESULT** values that includes network errors.

The networking APIs support different methods for retrieving this detailed information on the cause of an exception.

-   Some APIs provide a helper method that converts the **HRESULT** value from the exception to an enumeration value.
-   Other APIs provide a method to retrieve the actual **HRESULT** value.

## Related topics
* [Networking API Improvements in Windows 10](https://blogs.windows.com/buildingapps/2015/07/02/networking-api-improvements-in-windows-10/)