---
title: Networking for games
description: Learn how to develop and incorporate networking features into your DirectX game.
ms.assetid: 212eee15-045c-8ba1-e274-4532b2120c55
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, networking, directx
ms.localizationpriority: medium
---

# Networking for games

Learn how to develop and incorporate networking features into your DirectX game.

## Concepts at a glance

A variety of networking features can be used in your DirectX game, whether it is a simple standalone game to massively multi-player games. The simplest use of networking would be to store user names and game scores on a central network server.

Networking APIs are needed in multi-player games that use the infrastructure (client-server or internet peer-to-peer) model and also by ad hoc (local peer-to-peer) games. For server-based multi-player games, a central game server usually handles most of the game operations and the client game app is used for input, displaying graphics, playing audio, and other features. The speed and latency of network transfers is a concern for a satisfactory game experience.

For peer-to-peer games, each player's app handles the input and graphics. In most cases, the game players are located in close proximity so that network latency should be lower but is still a concern. How to discovery peers and establish a connection becomes a concern.

For single-player games, a central Web server or service is often used to store user names, game scores, and other miscellaneous information. In these games, the speed and latency of networking transfers is less of a concern since it doesn't directly affect game operation.

Network conditions can change at any time, so any game that uses networking APIs needs to handle network exceptions that may occur. To learn more about handling network exceptions, see [Networking basics](../networking/networking-basics.md).

Firewalls and web proxies are common and can affect the ability to use networking features. A game that uses networking needs to be prepared to properly handle firewalls and proxies.

For mobile devices, it is important to monitor available network resources and behave accordingly when on metered networks where roaming or data costs can be significant.

Network isolation is part of the app security model used by Windows. Windows actively discovers network boundaries and enforces network access restrictions for network isolation. Apps must declare network isolation capabilities in order to define the scope of network access. Without declaring these capabilities, your app will not have access to network resources. To learn more about how Windows enforces network isolation for apps, see [How to configure network isolation capabilities](/previous-versions/windows/apps/hh770532(v=win.10)).

## Design considerations

A variety of networking APIs can be used in DirectX games. So, it is important to pick the right API. Windows supports a variety of networking APIs that your app can use to communicate with other computers and devices over either the Internet or private networks. Your first step is to figure out what networking features your app needs.

These are the more popular network APIs for games.

-   TCP and sockets - Provides a reliable connection. Use TCP for game operations that don’t need security. TCP allows the server to easily scale, so it is commonly used in games that use the infrastructure (client-server or internet peer-to-peer) model. TCP can also be used by ad hoc (local peer-to-peer) games over Wi-Fi Direct and Bluetooth. TCP is commonly used for game object movement, character interaction, text chat, and other operations. The [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) class provides a TCP socket that can be used in Microsoft Store games. The **StreamSocket** class is used with related classes in the [**Windows::Networking::Sockets**](/uwp/api/Windows.Networking.Sockets) namespace.
-   TCP and sockets using SSL - Provides a reliable connection that prevents eavesdropping. Use TCP connections with SSL for game operations that need security. The encryption and overhead of SSL adds a cost in latency and performance, so it is only used when security is needed. TCP with SSL is commonly used for login, purchasing and trading assets, game character creation and management. The [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket) class provides a TCP socket that supports SSL.
-   UDP and sockets - Provides unreliable network transfers with low overhead. UDP is used for game operations that require low latency and can tolerate some packet loss. This is often used for fighting games, shooting and tracers, network audio, and voice chat. The [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket) class provides a UDP socket that can be used in Microsoft Store games. The **DatagramSocket** class is used with related classes in the [**Windows::Networking::Sockets**](/uwp/api/Windows.Networking.Sockets) namespace.
-   HTTP Client - Provides a reliable connection to HTTP servers. The most common networking scenario is to access a web site to retrieve or store information. A simple example would be a game that uses a website to store user information and game scores. When used with SSL for security, an HTTP client can be used for login, purchasing, trading assets, game character creation, and management. The [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) class provides a modern HTTP client API for use in Microsoft Store games. The **HttpClient** class is used with related classes in the [**Windows::Web::Http**](/uwp/api/Windows.Web.Http) namespace.

## Handling network exceptions in your DirectX game

When a network exception occurs in your DirectX game, this indicates a significant problem or failure. Exceptions can occur for many reasons when using networking APIs. Often, the exception can result from changes in network connectivity or other networking issues with the remote host or server.

Some causes of exceptions when using networking APIs include the following:

-   Input from the user for a hostname or a URI contains errors and is not valid.
-   Name resolutions failures when looking up a hostname or a URi.
-   Loss or change in network connectivity.
-   Network connection failures using sockets or the HTTP client APIs.
-   Network server or remote endpoint errors.
-   Miscellaneous networking errors.

Exceptions from network errors (for example, loss or change of connectivity, connection failures, and server failures) can happen at any time. These errors result in exceptions being thrown. If an exception is not handled by your app, it can cause your entire app to be terminated by the runtime.

You must write code to handle exceptions when you call most asynchronous network methods. Sometimes, when an exception occurs, a network method can be retried as a way to resolve the problem. Other times, your app may need to plan to continue without network connectivity using previously cached data.

Universal Windows Platform (UWP) apps generally throw a single exception. Your exception handler can retrieve more detailed information about the cause of the exception to better understand the failure and make appropriate decisions.

When an exception occurs in a DirectX game that is a UWP app, the **HRESULT** value for the cause of the error can be retrieved. The *Winerror.h* include file contains a large list of possible **HRESULT** values that includes network errors.

The networking APIs support different methods for retrieving this detailed information about the cause of an exception.

-   A method to retrieve the **HRESULT** value of the error that caused the exception. The possible list of potential **HRESULT** values is large and unspecified. The **HRESULT** value can be retrieved when using any of the networking APIs.
-   A helper method that converts the **HRESULT** value to an enumeration value. The list of possible enumeration values is specified and relatively small. A helper method is available for the socket classes in the [**Windows::Networking::Sockets**](/uwp/api/Windows.Networking.Sockets).

### Exceptions in Windows.Networking.Sockets

The constructor for the [**HostName**](/uwp/api/Windows.Networking.HostName) class used with sockets can throw an exception if the string passed is not a valid hostname (contains characters that are not allowed in a host name). If an app gets input from the user for the **HostName** for a peer connection for gaming, the constructor should be in a try/catch block. If an exception is thrown, the app can notify the user and request a new hostname.

Add code to validate a string for a hostname from the user

```cppcx
// Define some variables at the class level.
Windows::Networking::HostName^ remoteHost;

bool isHostnameFromUser = false;
bool isHostnameValid = false;

///...

// If the value of 'remoteHostname' is set by the user in a control as input 
// and is therefore untrusted input and could contain errors. 
// If we can't create a valid hostname, we notify the user in statusText 
// about the incorrect input.

String ^hostString = remoteHostname;

try 
{
    remoteHost = ref new Windows::Networking:Host(hostString);
    isHostnameValid = true;
}
catch (InvalidArgumentException ^ex)
{
    statusText->Text = "You entered a bad hostname, please re-enter a valid hostname.";
    return;
}

isHostnameFromUser = true;

// ... Continue with code to execute with a valid hostname.
```

The [**Windows.Networking.Sockets**](/uwp/api/Windows.Networking.Sockets) namespace has convenient helper methods and enumerations for handling errors when using sockets. This can be useful for handling specific network exceptions differently in your app.

An error encountered on [**DatagramSocket**](/uwp/api/Windows.Networking.Sockets.DatagramSocket), [**StreamSocket**](/uwp/api/Windows.Networking.Sockets.StreamSocket), or [**StreamSocketListener**](/uwp/api/Windows.Networking.Sockets.StreamSocketListener) operation results in an exception being thrown. The cause of the exception is an error value represented as an **HRESULT** value. The [**SocketError.GetStatus**](/uwp/api/windows.networking.sockets.socketerror.getstatus) method is used to convert a network error from a socket operation to a [**SocketErrorStatus**](/uwp/api/Windows.Networking.Sockets.SocketErrorStatus) enumeration value. Most of the **SocketErrorStatus** enumeration values correspond to an error returned by the native Windows sockets operation. An app can filter on specific **SocketErrorStatus** enumeration values to modify app behavior depending on the cause of the exception.

For parameter validation errors, an app can also use the **HRESULT** from the exception to learn more detailed information about the error that caused the exception. Possible **HRESULT** values are listed in the *Winerror.h* header file. For most parameter validation errors, the **HRESULT** returned is **E\_INVALIDARG**.

Add code to handle exceptions when trying to make a stream socket connection

```cppcx
using namespace Windows::Networking;
using namespace Windows::Networking::Sockets;
    
    // Define some more variables at the class level.

    bool isSocketConnected = false
    bool retrySocketConnect = false;

    // The number of times we have tried to connect the socket.
    unsigned int retryConnectCount = 0;

    // The maximum number of times to retry a connect operation.
    unsigned int maxRetryConnectCount = 5; 
    ///...

    // We pass in a valid remoteHost and serviceName parameter.
    // The hostname can contain a name or an IP address.
    // The servicename can contain a string or a TCP port number.

    StreamSocket ^ socket = ref new StreamSocket();
    SocketErrorStatus errorStatus; 
    HResult hr;

    // Save the socket, so any subsequent steps can use it.
    CoreApplication::Properties->Insert("clientSocket", socket);

    // Connect to the remote server. 
    create_task(socket->ConnectAsync(
            remoteHost,
            serviceName,
            SocketProtectionLevel::PlainSocket)).then([this] (task<void> previousTask)
    {
        try
        {
            // Try getting all exceptions from the continuation chain above this point.
            previousTask.get();

            isSocketConnected = true;
            // Mark the socket as connected. We do not really care about the value of the property, but the mere 
            // existence of  it means that we are connected.
            CoreApplication::Properties->Insert("connected", nullptr);
        }
        catch (Exception^ ex)
        {
            hr = ex.HResult;
            errorStatus = SocketStatus::GetStatus(hr); 
            if (errorStatus != Unknown)
            {
                                                                switch (errorStatus) 
                   {
                    case HostNotFound:
                        // If the hostname is from the user, this may indicate a bad input.
                        // Set a flag to ask the user to re-enter the hostname.
                        isHostnameValid = false;
                        return;
                        break;
                    case ConnectionRefused:
                        // The server might be temporarily busy.
                        retrySocketConnect = true;
                        return;
                        break; 
                    case NetworkIsUnreachable: 
                        // This could be a connectivity issue.
                        retrySocketConnect = true;
                        break;
                    case UnreachableHost: 
                        // This could be a connectivity issue.
                        retrySocketConnect = true;
                        break;
                    case NetworkIsDown: 
                        // This could be a connectivity issue.
                        retrySocketConnect = true;
                        break;
                    // Handle other errors. 
                    default: 
                        // The connection failed and no options are available.
                        // Try to use cached data if it is available. 
                        // You may want to tell the user that the connect failed.
                        break;
                }
                }
                else 
                {
                    // Received an Hresult that is not mapped to an enum.
                    // This could be a connectivity issue.
                    retrySocketConnect = true;
                }
            }
        });
    }

```

### Exceptions in Windows.Web.Http

The constructor for the [**Windows::Foundation::Uri**](/uwp/api/Windows.Foundation.Uri) class used with [**Windows::Web::Http::HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) can throw an exception if the string passed is not a valid URI (contains characters that are not allowed in a URI). In C++, there is no method to try and parse a string to a URI. If an app gets input from the user for the **Windows::Foundation::Uri**, the constructor should be in a try/catch block. If an exception is thrown, the app can notify the user and request a new URI.

Your app should also check that the scheme in the URI is HTTP or HTTPS since these are the only schemes supported by the [**Windows::Web::Http::HttpClient**](/uwp/api/Windows.Web.Http.HttpClient).

Add code to validate a string for a URI from the user

```cppcx
    // Define some variables at the class level.
    Windows::Foundation::Uri^ resourceUri;

    bool isUriFromUser = false;
    bool isUriValid = false;

    ///...

    // If the value of 'inputUri' is set by the user in a control as input 
    // and is therefore untrusted input and could contain errors. 
    // If we can't create a valid hostname, we notify the user in statusText 
    // about the incorrect input.

    String ^uriString = inputUri;

    try 
    {
        isUriValid = false;
        resourceUri = ref new Windows::Foundation:Uri(uriString);

        if (resourceUri->SchemeName != "http" && resourceUri->SchemeName != "https")
        {
            statusText->Text = "Only 'http' and 'https' schemes supported. Please re-enter URI";
            return;
        }
        isUriValid = true;
    }
    catch (InvalidArgumentException ^ex)
    {
        statusText->Text = "You entered a bad URI, please re-enter Uri to continue.";
        return;
    }

    isUriFromUser = true;


    // ... Continue with code to execute with a valid URI.
```

The [**Windows::Web::Http**](/uwp/api/windows.web.http) namespace lacks a convenience function. So, an app using [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) and other classes in this namespace needs to use the **HRESULT** value.

In apps using C++, the [**Platform::Exception**](/cpp/cppcx/platform-exception-class) represents an error during app execution when an exception occurs. The [**Platform::Exception::HResult**](/cpp/cppcx/platform-exception-class#hresult) property returns the **HRESULT** assigned to the specific exception. The [**Platform::Exception::Message**](/cpp/cppcx/platform-exception-class#message) property returns the system-provided string that is associated with the **HRESULT** value. Possible **HRESULT** values are listed in the *Winerror.h* header file. An app can filter on specific **HRESULT** values to modify app behavior depending on the cause of the exception.

For most parameter validation errors, the **HRESULT** returned is **E\_INVALIDARG**. For some illegal method calls, the **HRESULT** returned is **E\_ILLEGAL\_METHOD\_CALL**.

Add code to handle exceptions when trying to use [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) to connect to an HTTP server

```cppcx
using namespace Windows::Foundation;
using namespace Windows::Web::Http;
    
    // Define some more variables at the class level.

    bool isHttpClientConnected = false
    bool retryHttpClient = false;

    // The number of times we have tried to connect the socket
    unsigned int retryConnectCount = 0;

    // The maximum number of times to retry a connect operation.
    unsigned int maxRetryConnectCount = 5; 
    ///...

    // We pass in a valid resourceUri parameter.
    // The URI must contain a scheme and a name or an IP address.

    HttpClient ^ httpClient = ref new HttpClient();
    HResult hr;

    // Save the httpClient, so any subsequent steps can use it.
    CoreApplication::Properties->Insert("httpClient", httpClient);

    // Send a GET request to the HTTP server. 
    create_task(httpClient->GetAsync(resourceUri)).then([this] (task<void> previousTask)
    {
        try
        {
            // Try getting all exceptions from the continuation chain above this point.
            previousTask.get();

            isHttpClientConnected = true;
            // Mark the HttClient as connected. We do not really care about the value of the property, but the mere 
            // existence of  it means that we are connected.
            CoreApplication::Properties->Insert("connected", nullptr);
        }
        catch (Exception^ ex)
        {
            hr = ex.HResult;
                                                switch (errorStatus) 
               {
                case WININET_E_NAME_NOT_RESOLVED:
                    // If the Uri is from the user, this may indicate a bad input.
                    // Set a flag to ask user to re-enter the Uri.
                    isUriValid = false;
                    return;
                    break;
                case WININET_E_CANNOT_CONNECT:
                    // The server might be temporarily busy.
                    retryHttpClientConnect = true;
                    return;
                    break; 
                case WININET_E_CONNECTION_ABORTED: 
                    // This could be a connectivity issue.
                    retryHttpClientConnect = true;
                    break;
                case WININET_E_CONNECTION_RESET: 
                    // This could be a connectivity issue.
                    retryHttpClientConnect = true;
                    break;
                case INET_E_RESOURCE_NOT_FOUND: 
                    // The server cannot locate the resource specified in the uri.
                    // If the Uri is from user, this may indicate a bad input.
                    // Set a flag to ask the user to re-enter the Uri
                    isUriValid = false;
                    return;
                    break;
                // Handle other errors. 
                default: 
                    // The connection failed and no options are available.
                    // Try to use cached data if it is available. 
                    // You may want to tell the user that the connect failed.
                    break;
            }
            else 
            {
                // Received an Hresult that is not mapped to an enum.
                // This could be a connectivity issue.
                retrySocketConnect = true;
            }
        }
    });
```

## Related topics

### Other resources

* [Connecting with a datagram socket](/previous-versions/windows/apps/jj635238(v=win.10))
* [Connecting to a network resource with a stream socket](/previous-versions/windows/apps/jj150599(v=win.10))
* [Connecting to network services](/previous-versions/windows/apps/hh452976(v=win.10))
* [Connecting to web services](/previous-versions/windows/apps/hh761504(v=win.10))
* [Networking basics](../networking/networking-basics.md)
* [How to configure network isolation capabilities](/previous-versions/windows/apps/hh770532(v=win.10))
* [How to enable loopback and debug network isolation](/previous-versions/windows/apps/hh780593(v=win.10))

### Reference

* [DatagramSocket](/uwp/api/Windows.Networking.Sockets.DatagramSocket)
* [HttpClient](/uwp/api/Windows.Web.Http.HttpClient)
* [StreamSocket](/uwp/api/Windows.Networking.Sockets.StreamSocket)
* [Windows::Web::Http namespace](/uwp/api/Windows.Web.Http)
* [Windows::Networking::Sockets namespace](/uwp/api/Windows.Networking.Sockets)

### Sample apps

* [DatagramSocket sample](/samples/microsoft/windows-universal-samples/datagramsocket/)
* [HttpClient sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/HttpClient%20sample)
* [Proximity sample](https://github.com/microsoft/VCSamples/tree/master/VC2012Samples/Windows%208%20samples/C%2B%2B/Windows%208%20app%20samples/Proximity%20sample%20(Windows%208))
* [StreamSocket sample](/samples/microsoft/windows-universal-samples/streamsocket/)