---
title: Error handling
author: KevinAsgari
description: Learn how to handle errors when making an Xbox Live service call.
ms.assetid: e433dfbd-488b-44ff-8333-1dcf0329cd60
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, error, service call
ms.localizationpriority: low
---

# Error handling

Developers should take special care when making a service call. Your title should always handle errors when making network calls. Even for service calls that have been working consistently during development, service calls can fail in real world environments for a number of different reasons. For example, your call may fail due to:

* The network is not available.
* The service is too busy, returning a 503 HTTP status code.
* The request is not valid, returning a 400 HTTP status code.
* The user does not have the right permissions, returning a 403 HTTP status code.
* The user has been banned, returning a 401 HTTP status code.
IXMLHTTPRequest2, which is called by the WinRT service APIs, is unable to send the request (ERROR_TIMEOUT, RPC_S_CALL_FAILED_DNE, etc.)
* The list above is not exhaustive. The majority of these issues will result in an exception being thrown from the Services API layer. Your title should capture these exceptions and handle them appropriately.

There a two different styles of error handling in Xbox Services API (XSAPI) depending on the API you are using:

See [C++ API Error Handling](error-handling-cpp.md).

See [WinRT API Error Handling](error-handling-winrt.md).
