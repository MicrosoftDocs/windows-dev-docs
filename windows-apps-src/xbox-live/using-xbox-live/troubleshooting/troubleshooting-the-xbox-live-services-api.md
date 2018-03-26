---
title: Troubleshooting the Xbox Live Services API
author: KevinAsgari
description: Learn how to log extra error information while troubleshooting issues with the Xbox Live APIs.
ms.assetid: 3827bba1-902f-4f2d-ad51-af09bd9354c4
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, troubleshooting, error, log
ms.localizationpriority: low
---
# Troubleshooting the Xbox Live APIs

## Code

It is difficult to diagnose a failure using only the error from the Xbox Live Services API layer. Extra error information—such as logging of all RESTful calls—could be available to the server. To listen to this data, hook up the response logger and enable debug tracing. Response logging allows you to see HTTP traffic and web service response codes, which is often as useful as a Fiddler trace.

### C++

The following code example enables response logging and sets the debug error level to Verbose (you can also set the debug error level to Error to show only trace failed calls, or to Off to disable tracing). The resulting debug output is sent to the Output pane when running your project in Visual Studio.  

```cpp

        // Set up debug tracing to the Output window in Visual Studio.
            xbox::services::system::xbox_live_services_settings::get_singleton_instance()->set_diagnostics_trace_level(
                xbox_services_diagnostics_trace_level::verbose
                );
```

You can also choose to redirect debug output to your own log file like so:

```cpp

        // Set up debug tracing of the Xbox Live Services API traffic to the game UI.
        m_xboxLiveContext->Settings->EnableServiceCallRoutedEvents = true;
        m_xboxLiveContext->Settings->ServiceCallRouted += ref new
        Windows::Foundation::EventHandler<Microsoft::Xbox::Services::XboxServiceCallRoutedEventArgs^>(
            [=] ( Platform::Object^, Microsoft::Xbox::Services::XboxServiceCallRoutedEventArgs^ args )
            {
                gameUI->Log(L"[URL]: " + args->HttpMethod + " " + args->Url->AbsoluteUri);
                gameUI->Log(L"");
                gameUI->Log(L"[Response]: " + args->HttpStatus.ToString() + " " + args->ResponseBody);
            });

```
