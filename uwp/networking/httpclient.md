---
description: Use HttpClient and the rest of the Windows.Web.Http namespace API to send and receive information using the HTTP 2.0 and HTTP 1.1 protocols.
title: HttpClient
ms.assetid: EC9820D3-3A46-474F-8A01-AE1C27442750
ms.date: 06/05/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# HttpClient

**Important APIs**

-   [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient)
-   [**Windows.Web.Http**](/uwp/api/Windows.Web.Http)
-   [**Windows.Web.Http.HttpResponseMessage**](/uwp/api/Windows.Web.Http.HttpResponseMessage)

Use [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) and the rest of the [**Windows.Web.Http**](/uwp/api/Windows.Web.Http) namespace API to send and receive information using the HTTP 2.0 and HTTP 1.1 protocols.

## Overview of HttpClient and the Windows.Web.Http namespace

The classes in the [**Windows.Web.Http**](/uwp/api/Windows.Web.Http) namespace and the related [**Windows.Web.Http.Headers**](/uwp/api/Windows.Web.Http.Headers) and [**Windows.Web.Http.Filters**](/uwp/api/Windows.Web.Http.Filters) namespaces provide a programming interface for Universal Windows Platform (UWP) apps that act as an HTTP client to perform basic GET requests or implement more advanced HTTP functionality listed below.

-   Methods for common verbs (**DELETE**, **GET**, **PUT**, and **POST**). Each of these requests are sent as an asynchronous operation.

-   Support for common authentication settings and patterns.

-   Access to Secure Sockets Layer (SSL) details on the transport.

-   Ability to include customized filters in advanced apps.

-   Ability to get, set, and delete cookies.

-   HTTP Request progress info available on asynchronous methods.

The [**Windows.Web.Http.HttpRequestMessage**](/uwp/api/Windows.Web.Http.HttpRequestMessage) class represents an HTTP request message sent by [**Windows.Web.Http.HttpClient**](/uwp/api/Windows.Web.Http.HttpClient). The [**Windows.Web.Http.HttpResponseMessage**](/uwp/api/Windows.Web.Http.HttpResponseMessage) class represents an HTTP response message received from an HTTP request. HTTP messages are defined in [RFC 2616](https://tools.ietf.org/html/rfc2616) by the IETF.

The [**Windows.Web.Http**](/uwp/api/Windows.Web.Http) namespace represents HTTP content as the HTTP entity body and headers including cookies. HTTP content can be associated with an HTTP request or an HTTP response. The **Windows.Web.Http** namespace provides a number of different classes to represent HTTP content.

-   [**HttpBufferContent**](/uwp/api/Windows.Web.Http.HttpBufferContent). Content as a buffer
-   [**HttpFormUrlEncodedContent**](/uwp/api/Windows.Web.Http.HttpFormUrlEncodedContent). Content as name and value tuples encoded with the **application/x-www-form-urlencoded** MIME type
-   [**HttpMultipartContent**](/uwp/api/Windows.Web.Http.HttpMultipartContent). Content in the form of the **multipart/\*** MIME type.
-   [**HttpMultipartFormDataContent**](/uwp/api/Windows.Web.Http.HttpMultipartFormDataContent). Content that is encoded as the **multipart/form-data** MIME type.
-   [**HttpStreamContent**](/uwp/api/Windows.Web.Http.HttpStreamContent). Content as a stream (the internal type is used by the HTTP GET method to receive data and the HTTP POST method to upload data)
-   [**HttpStringContent**](/uwp/api/Windows.Web.Http.HttpStringContent). Content as a string.
-   [**IHttpContent**](/uwp/api/Windows.Web.Http.IHttpContent) - A base interface for developers to create their own content objects

The code snippet in the "Send a simple GET request over HTTP" section uses the [**HttpStringContent**](/uwp/api/Windows.Web.Http.HttpStringContent) class to represent the HTTP response from an HTTP GET request as a string.

The [**Windows.Web.Http.Headers**](/uwp/api/Windows.Web.Http.Headers) namespace supports creation of HTTP headers and cookies, which are then associated as properties with [**HttpRequestMessage**](/uwp/api/Windows.Web.Http.HttpRequestMessage) and [**HttpResponseMessage**](/uwp/api/Windows.Web.Http.HttpResponseMessage) objects.

## Send a simple GET request over HTTP

As mentioned earlier in this article, the [**Windows.Web.Http**](/uwp/api/Windows.Web.Http) namespace allows UWP apps to send GET requests. The following code snippet demonstrates how to send a GET request to `http://www.contoso.com` using the [**Windows.Web.Http.HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) class and the [**Windows.Web.Http.HttpResponseMessage**](/uwp/api/Windows.Web.Http.HttpResponseMessage) class to read the response from the GET request.

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

```cppwinrt
// pch.h
#pragma once
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Web.Http.Headers.h>

// main.cpp : Defines the entry point for the console application.
#include "pch.h"
#include <iostream>
using namespace winrt;
using namespace Windows::Foundation;

int main()
{
    init_apartment();

    // Create an HttpClient object.
    Windows::Web::Http::HttpClient httpClient;

    // Add a user-agent header to the GET request.
    auto headers{ httpClient.DefaultRequestHeaders() };

    // The safe way to add a header value is to use the TryParseAdd method, and verify the return value is true.
    // This is especially important if the header value is coming from user input.
    std::wstring header{ L"ie" };
    if (!headers.UserAgent().TryParseAdd(header))
    {
        throw L"Invalid header value: " + header;
    }

    header = L"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
    if (!headers.UserAgent().TryParseAdd(header))
    {
        throw L"Invalid header value: " + header;
    }

    Uri requestUri{ L"http://www.contoso.com" };

    // Send the GET request asynchronously, and retrieve the response as a string.
    Windows::Web::Http::HttpResponseMessage httpResponseMessage;
    std::wstring httpResponseBody;

    try
    {
        // Send the GET request.
        httpResponseMessage = httpClient.GetAsync(requestUri).get();
        httpResponseMessage.EnsureSuccessStatusCode();
        httpResponseBody = httpResponseMessage.Content().ReadAsStringAsync().get();
    }
    catch (winrt::hresult_error const& ex)
    {
        httpResponseBody = ex.message();
    }
    std::wcout << httpResponseBody;
}
```

## POST binary data over HTTP

The [C++/WinRT](../cpp-and-winrt-apis/index.md) code example below illustrates using form data and a POST request to send a small amount of binary data as a file upload to a web server. The code uses the [**HttpBufferContent**](/uwp/api/windows.web.http.httpbuffercontent) class to represent the binary data, and the [**HttpMultipartFormDataContent**](/uwp/api/windows.web.http.httpmultipartformdatacontent) class to represent the multi-part form data.

> [!NOTE]
> Calling **get** (as seen in the code example below) isn't appropriate for a UI thread. For the correct technique to use in that case, see [Concurrency and asynchronous operations with C++/WinRT](../cpp-and-winrt-apis/concurrency.md).

```cppwinrt
// pch.h
#pragma once
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Security.Cryptography.h>
#include <winrt/Windows.Storage.Streams.h>
#include <winrt/Windows.Web.Http.Headers.h>

// main.cpp : Defines the entry point for the console application.
#include "pch.h"
#include <iostream>
#include <sstream>
using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;

int main()
{
    init_apartment();

    auto buffer{
        Windows::Security::Cryptography::CryptographicBuffer::ConvertStringToBinary(
            L"A sentence of text to encode into binary to serve as sample data.",
            Windows::Security::Cryptography::BinaryStringEncoding::Utf8
        )
    };
    Windows::Web::Http::HttpBufferContent binaryContent{ buffer };
    // You can use the 'image/jpeg' content type to represent any binary data;
    // it's not necessarily an image file.
    binaryContent.Headers().Append(L"Content-Type", L"image/jpeg");

    Windows::Web::Http::Headers::HttpContentDispositionHeaderValue disposition{ L"form-data" };
    binaryContent.Headers().ContentDisposition(disposition);
    // The 'name' directive contains the name of the form field representing the data.
    disposition.Name(L"fileForUpload");
    // Here, the 'filename' directive is used to indicate to the server a file name
    // to use to save the uploaded data.
    disposition.FileName(L"file.dat");

    Windows::Web::Http::HttpMultipartFormDataContent postContent;
    postContent.Add(binaryContent); // Add the binary data content as a part of the form data content.

    // Send the POST request asynchronously, and retrieve the response as a string.
    Windows::Web::Http::HttpResponseMessage httpResponseMessage;
    std::wstring httpResponseBody;

    try
    {
        // Send the POST request.
        Uri requestUri{ L"https://www.contoso.com/post" };
        Windows::Web::Http::HttpClient httpClient;
        httpResponseMessage = httpClient.PostAsync(requestUri, postContent).get();
        httpResponseMessage.EnsureSuccessStatusCode();
        httpResponseBody = httpResponseMessage.Content().ReadAsStringAsync().get();
    }
    catch (winrt::hresult_error const& ex)
    {
        httpResponseBody = ex.message();
    }
    std::wcout << httpResponseBody;
}
```

To POST the contents of an actual binary file (rather than the explicit binary data used above), you'll find it easier to use an [HttpStreamContent](/uwp/api/windows.web.http.httpstreamcontent) object. Construct one and, as the argument to its constructor, pass the value returned from a call to [StorageFile.OpenReadAsync](/uwp/api/windows.storage.storagefile.openreadasync). That method returns a stream for the data inside your binary file.

Also, if you're uploading a large file (larger than about 10MB), then we recommend that you use the Windows Runtime [Background Transfer](/uwp/api/windows.networking.backgroundtransfer) APIs.

## POST JSON data over HTTP

The following example posts some JSON to an endpoint, then writes out the response body.

```cs
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

private async Task TryPostJsonAsync()
{
    try
    {
        // Construct the HttpClient and Uri. This endpoint is for test purposes only.
        HttpClient httpClient = new HttpClient();
        Uri uri = new Uri("https://www.contoso.com/post");

        // Construct the JSON to post.
        HttpStringContent content = new HttpStringContent(
            "{ \"firstName\": \"Eliot\" }",
            UnicodeEncoding.Utf8,
            "application/json");

        // Post the JSON and wait for a response.
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
            uri,
            content);

        // Make sure the post succeeded, and write out the response.
        httpResponseMessage.EnsureSuccessStatusCode();
        var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
        Debug.WriteLine(httpResponseBody);
    }
    catch (Exception ex)
    {
        // Write out any exceptions.
        Debug.WriteLine(ex);
    }
}
```

```cppwinrt
// pch.h
#pragma once
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Security.Cryptography.h>
#include <winrt/Windows.Storage.Streams.h>
#include <winrt/Windows.Web.Http.Headers.h>

// main.cpp : Defines the entry point for the console application.
#include "pch.h"
#include <iostream>
#include <sstream>
using namespace winrt;
using namespace Windows::Foundation;
using namespace Windows::Storage::Streams;

int main()
{
    init_apartment();

    Windows::Web::Http::HttpResponseMessage httpResponseMessage;
    std::wstring httpResponseBody;

    try
    {
        // Construct the HttpClient and Uri. This endpoint is for test purposes only.
        Windows::Web::Http::HttpClient httpClient;
        Uri requestUri{ L"https://www.contoso.com/post" };

        // Construct the JSON to post.
        Windows::Web::Http::HttpStringContent jsonContent(
            L"{ \"firstName\": \"Eliot\" }",
            UnicodeEncoding::Utf8,
            L"application/json");

        // Post the JSON, and wait for a response.
        httpResponseMessage = httpClient.PostAsync(
            requestUri,
            jsonContent).get();

        // Make sure the post succeeded, and write out the response.
        httpResponseMessage.EnsureSuccessStatusCode();
        httpResponseBody = httpResponseMessage.Content().ReadAsStringAsync().get();
        std::wcout << httpResponseBody.c_str();
    }
    catch (winrt::hresult_error const& ex)
    {
        std::wcout << ex.message().c_str();
    }
}
```

## Exceptions in Windows.Web.Http

An exception is thrown when an invalid string for a the Uniform Resource Identifier (URI) is passed to the constructor for the [**Windows.Foundation.Uri**](/uwp/api/Windows.Foundation.Uri) object.

**.NET:**  The [**Windows.Foundation.Uri**](/uwp/api/Windows.Foundation.Uri) type appears as [**System.Uri**](/dotnet/api/system.uri) in C# and VB.

In C# and Visual Basic, this error can be avoided by using the [**System.Uri**](/dotnet/api/system.uri) class in the .NET 4.5 and one of the [**System.Uri.TryCreate**](/dotnet/api/system.uri.trycreate#overloads) methods to test the string received from a user before the URI is constructed.

In C++, there is no method to try and parse a string to a URI. If an app gets input from the user for the [**Windows.Foundation.Uri**](/uwp/api/Windows.Foundation.Uri), the constructor should be in a try/catch block. If an exception is thrown, the app can notify the user and request a new hostname.

The [**Windows.Web.Http**](/uwp/api/Windows.Web.Http) lacks a convenience function. So an app using [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) and other classes in this namespace needs to use the **HRESULT** value.

In apps using [C++/WinRT](../cpp-and-winrt-apis/index.md), the [**winrt::hresult_error**](/uwp/cpp-ref-for-winrt/error-handling/hresult-error) struct represents an exception raised during app execution. The [winrt::hresult_error::code](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresult_errorcode-function) function returns the **HRESULT** assigned to the specific exception. The [winrt::hresult_error::message](/uwp/cpp-ref-for-winrt/error-handling/hresult-error#hresult_errormessage-function) function returns the system-provided string that is associated with the **HRESULT** value. For more info, see [Error handling with C++/WinRT](../cpp-and-winrt-apis/error-handling.md)

Possible **HRESULT** values are listed in the *Winerror.h* header file. Your app can filter on specific **HRESULT** values to modify app behavior depending on the cause of the exception.

In apps using the .NET Framework 4.5 in C#, VB.NET, the [System.Exception](/dotnet/api/system.exception) represents an error during app execution when an exception occurs. The [System.Exception.HResult](/dotnet/api/system.exception.hresult#System_Exception_HResult) property returns the **HRESULT** assigned to the specific exception. The [System.Exception.Message](/dotnet/api/system.exception.message#System_Exception_Message) property returns the message that describes the exception.

C++/CX has been superseded by [C++/WinRT](../cpp-and-winrt-apis/index.md). But in apps using C++/CX, the [Platform::Exception](/cpp/cppcx/platform-exception-class) represents an error during app execution when an exception occurs. The [Platform::Exception::HResult](/cpp/cppcx/platform-exception-class#hresult) property returns the **HRESULT** assigned to the specific exception. The [Platform::Exception::Message](/cpp/cppcx/platform-exception-class#message) property returns the system-provided string that is associated with the **HRESULT** value.

For most parameter validation errors, the **HRESULT** returned is **E\_INVALIDARG**. For some illegal method calls, the **HRESULT** returned is **E\_ILLEGAL\_METHOD\_CALL**.
