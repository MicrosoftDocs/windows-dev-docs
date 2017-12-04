---
author: stevewhims
description: Use HttpClient and the rest of the Windows.Web.Http namespace API to send and receive information using the HTTP 2.0 and HTTP 1.1 protocols.
title: HttpClient
ms.assetid: EC9820D3-3A46-474F-8A01-AE1C27442750
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# HttpClient


**Important APIs**

-   [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639)
-   [**Windows.Web.Http**](https://msdn.microsoft.com/library/windows/apps/dn279692)
-   [**Windows.Web.Http.HttpResponseMessage**](https://msdn.microsoft.com/library/windows/apps/dn279631)

Use [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) and the rest of the [**Windows.Web.Http**](https://msdn.microsoft.com/library/windows/apps/dn279692) namespace API to send and receive information using the HTTP 2.0 and HTTP 1.1 protocols.

## Overview of HttpClient and the Windows.Web.Http namespace

The classes in the [**Windows.Web.Http**](https://msdn.microsoft.com/library/windows/apps/dn279692) namespace and the related [**Windows.Web.Http.Headers**](https://msdn.microsoft.com/library/windows/apps/dn252713) and [**Windows.Web.Http.Filters**](https://msdn.microsoft.com/library/windows/apps/dn298623) namespaces provide a programming interface for Universal Windows Platform (UWP) apps that act as an HTTP client to perform basic GET requests or implement more advanced HTTP functionality listed below.

-   Methods for common verbs (**DELETE**, **GET**, **PUT**, and **POST**). Each of these requests are sent as an asynchronous operation.

-   Support for common authentication settings and patterns.

-   Access to Secure Sockets Layer (SSL) details on the transport.

-   Ability to include customized filters in advanced apps.

-   Ability to get, set, and delete cookies.

-   HTTP Request progress info available on asynchronous methods.

The [**Windows.Web.Http.HttpRequestMessage**](https://msdn.microsoft.com/library/windows/apps/dn279617) class represents an HTTP request message sent by [**Windows.Web.Http.HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639). The [**Windows.Web.Http.HttpResponseMessage**](https://msdn.microsoft.com/library/windows/apps/dn279631) class represents an HTTP response message received from an HTTP request. HTTP messages are defined in [RFC 2616](http://go.microsoft.com/fwlink/p/?linkid=241642) by the IETF.

The [**Windows.Web.Http**](https://msdn.microsoft.com/library/windows/apps/dn279692) namespace represents HTTP content as the HTTP entity body and headers including cookies. HTTP content can be associated with an HTTP request or an HTTP response. The **Windows.Web.Http** namespace provides a number of different classes to represent HTTP content.

-   [**HttpBufferContent**](https://msdn.microsoft.com/library/windows/apps/dn298625). Content as a buffer
-   [**HttpFormUrlEncodedContent**](https://msdn.microsoft.com/library/windows/apps/dn298685). Content as name and value tuples encoded with the **application/x-www-form-urlencoded** MIME type
-   [**HttpMultipartContent**](https://msdn.microsoft.com/library/windows/apps/dn298708). Content in the form of the **multipart/\*** MIME type.
-   [**HttpMultipartFormDataContent**](https://msdn.microsoft.com/library/windows/apps/dn279596). Content that is encoded as the **multipart/form-data** MIME type.
-   [**HttpStreamContent**](https://msdn.microsoft.com/library/windows/apps/dn279649). Content as a stream (the internal type is used by the HTTP GET method to receive data and the HTTP POST method to upload data)
-   [**HttpStringContent**](https://msdn.microsoft.com/library/windows/apps/dn279661). Content as a string.
-   [**IHttpContent**](https://msdn.microsoft.com/library/windows/apps/dn279684) - A base interface for developers to create their own content objects

The code snippet in the "Send a simple GET request over HTTP" section uses the [**HttpStringContent**](https://msdn.microsoft.com/library/windows/apps/dn279661) class to represent the HTTP response from an HTTP GET request as a string.

The [**Windows.Web.Http.Headers**](https://msdn.microsoft.com/library/windows/apps/dn252713) namespace supports creation of HTTP headers and cookies, which are then associated as properties with [**HttpRequestMessage**](https://msdn.microsoft.com/library/windows/apps/dn279617) and [**HttpResponseMessage**](https://msdn.microsoft.com/library/windows/apps/dn279631) objects.

## Send a simple GET request over HTTP

As mentioned earlier in this article, the [**Windows.Web.Http**](https://msdn.microsoft.com/library/windows/apps/dn279692) namespace allows UWP apps to send GET requests. The following code snippet demonstrates how to send a GET request to http://www.contoso.com using the [**Windows.Web.Http.HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) class and the [**Windows.Web.Http.HttpResponseMessage**](https://msdn.microsoft.com/library/windows/apps/dn279631) class to read the response from the GET request.

```csharp
//Create an HTTP client object
Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

//Add a user-agent header to the GET request. 
var headers = httpClient.DefaultRequestHeaders;

//The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
//especially if the header value is coming from user input.
string header = "ie";
if (!headers.UserAgent.TryParseAdd(header))
{
    throw new Exception("Invalid header value: " + header);
}

header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
if (!headers.UserAgent.TryParseAdd(header))
{
    throw new Exception("Invalid header value: " + header);
}

Uri requestUri = new Uri("http://www.contoso.com");

//Send the GET request asynchronously and retrieve the response as a string.
Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
string httpResponseBody = "";

try
{
    //Send the GET request
    httpResponse = await httpClient.GetAsync(requestUri);
    httpResponse.EnsureSuccessStatusCode();
    httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
}
catch (Exception ex)
{
    httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
}
```

## Exceptions in Windows.Web.Http

An exception is thrown when an invalid string for a the Uniform Resource Identifier (URI) is passed to the constructor for the [**Windows.Foundation.Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) object.

**.NET:**  The [**Windows.Foundation.Uri**](https://msdn.microsoft.com/library/windows/apps/br225998) type appears as [**System.Uri**](https://msdn.microsoft.com/library/windows/apps/xaml/system.uri.aspx) in C# and VB.

In C# and Visual Basic, this error can be avoided by using the [**System.Uri**](https://msdn.microsoft.com/library/windows/apps/xaml/system.uri.aspx) class in the .NET 4.5 and one of the [**System.Uri.TryCreate**](https://msdn.microsoft.com/library/windows/apps/xaml/system.uri.trycreate.aspx) methods to test the string received from a user before the URI is constructed.

In C++, there is no method to try and parse a string to a URI. If an app gets input from the user for the [**Windows.Foundation.Uri**](https://msdn.microsoft.com/library/windows/apps/br225998), the constructor should be in a try/catch block. If an exception is thrown, the app can notify the user and request a new hostname.

The [**Windows.Web.Http**](https://msdn.microsoft.com/library/windows/apps/dn279692) lacks a convenience function. So an app using [**HttpClient**](https://msdn.microsoft.com/library/windows/apps/dn298639) and other classes in this namespace needs to use the **HRESULT** value.

In apps using the .NET Framework 4.5 in C#, VB.NET, the [System.Exception](http://msdn.microsoft.com/library/system.exception.aspx) represents an error during app execution when an exception occurs. The [System.Exception.HResult](http://msdn.microsoft.com/library/system.exception.hresult.aspx) property returns the **HRESULT** assigned to the specific exception. The [System.Exception.Message](http://msdn.microsoft.com/library/system.exception.message.aspx) property returns the message that describes the exception. Possible **HRESULT** values are listed in the *Winerror.h* header file. An app can filter on specific **HRESULT** values to modify app behavior depending on the cause of the exception.

In apps using managed C++, the [Platform::Exception](http://msdn.microsoft.com/library/windows/apps/hh755825.aspx) represents an error during app execution when an exception occurs. The [Platform::Exception::HResult](http://msdn.microsoft.com/library/windows/apps/hh763371.aspx) property returns the **HRESULT** assigned to the specific exception. The [Platform::Exception::Message](http://msdn.microsoft.com/library/windows/apps/hh763375.aspx) property returns the system-provided string that is associated with the **HRESULT** value. Possible **HRESULT** values are listed in the *Winerror.h* header file. An app can filter on specific **HRESULT** values to modify app behavior depending on the cause of the exception.

For most parameter validation errors, the **HRESULT** returned is **E\_INVALIDARG**. For some illegal method calls, the **HRESULT** returned is **E\_ILLEGAL\_METHOD\_CALL**.

