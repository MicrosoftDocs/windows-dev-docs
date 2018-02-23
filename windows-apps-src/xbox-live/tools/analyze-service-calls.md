---
title: Xbox Live Trace Analyzer
author: KevinAsgari
description: Learn how to use the Xbox Live Trace Analyzer to review the service calls made by your title.
ms.assetid: b4490fae-d554-403d-bbbc-601af38af0ef
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, service calls, testing, trace analyzer
ms.localizationpriority: low
---

# Xbox Live Trace Analyzer

The Xbox Live Services API now allows title developers to capture all service calls and then analyze them offline for any violations in calling patterns. Service call tracing can be activated by using new functionality available in the xbtrace command line tool, or through protocol activation for more advanced scenarios. Activating service call tracking directly from title code is also supported. The offline analysis tool, called the Xbox Live Trace Analyzer (XBLTraceAnalyzer.exe) can be found as part of the Xbox Live Tools package from [https://aka.ms/xboxliveuwptools](https://aka.ms/xboxliveuwptools).


## Gather logs and analyze the service calls

The following steps are required to gather the logs that contain the record of your service calls and analyze them using Xbox Live Trace Analyzer.

1.  Build your title using the version of the Xbox Live Services API that is included in the July 2015 or newer version of the Xbox Developer Kit (XDK).
2.  Modify your title to enable tracing as described below.
3.  Deploy your title.
4.  Launch the title and make at least one call to Xbox Live Services in order to initialize the Xbox Live Services API.
5.  Start tracing at the point in your title you'd like to analyze.
6.  Stop tracing.
7.  Run the Xbox Live Trace Analyzer tool on your development PC and view the output.

## Starting and stopping tracing

There are three ways to start and stop tracing:

1.  You can call a set of Xbox Live Services APIs directly from your title.
2.  You can use the *xbtrace* command line tool.
3.  You can use protocol activation through the *Application Management (xbapp.exe)* command line tool.


### Starting and stopping tracing directly from your title

To start tracing directly from your title, you must do the following:

1.  In the `Microsoft::Xbox::Services::Experimental` namespace, set the `EnableServiceCallTracking` property of the `ServiceCallTrackerSettings` class to true.
2.  Call `StartServiceCallTracking()` to start tracing service calls.
3.  Call `StopServiceCallTracking()` to stop tracing service calls.
4.  After tracing is stopped, copy the resulting trace file from the developer scratch drive on the console back to your PC by using either *File Copy (xbcp.exe)* or the *Xbox One Neighborhood* in order to analyze it by using Xbox Live Trace Analyzer.

### Starting and stopping tracing by using the xbtrace command line tool

The most convenient and straightforward way to start tracing is to use the xbtrace command line tool with the xboxliveservices trace type. When you use xbTrace, the resulting trace file is copied back to your PC for you.

Starting and stopping traces using xbtrace relies on protocol activation. Before using xbtrace to start and stop tracing, you must initialize protocol activation by calling the `RegisterForProtcolActivation` method on the `ServiceCallTrackerSettings` class.

The following example shows how to start and stop an Xbox Live Services trace by using xbTrace:

    xbtrace start xboxliveservices
    xbtrace stop


Remember that your title must be running and protocol activation must be initialized before you can start and stop tracing with xbtrace. After tracing is stopped, xbtrace copies the trace file to your development PC and places it in a directory whose name includes "xbtrace" and a timestamp. The name of this directory can be overridden using \[etlfile\] option to xbtrace.

Starting and stopping tracing by using protocol activation
----------------------------------------------------------

Tracing can also be controlled by using the protocol activation features of "xbApp launch". You must know your title's titleid to start and stop tracing via protocol activation. You can find your title id in your title's manifest file. Tracing is controlled through URIs that contain "serviceCallTracking" parameter. The following examples show how to start and stop tracing for a title whose title id is 12345678:

    xbapp launch "ms-xbl-12345678://serviceCallTracking?state=start"
    xbapp launch "ms-xbl-12345678://serviceCallTracking?state=stop"

When you use protocol activation, the resulting trace file is stored on the developer scratch drive on the console. You'll need to copy the file back to your PC using either xbcp or the Xbox One Neighborhood. The file is not automatically copied back to the PC as it is when using xbtrace.

Protocol activation allows you to set additional trace parameters, such as a verbosity. Four levels of verbosity are supported: quiet, diagnostic, detailed and minimal. The following example shows how to set a verbosity level:

    xbapp launch "ms-xbl-12345678://serviceCallTracking?verbosity=diagnostic"

## Analyze the trace file

After the trace file has been copied back to your PC, you can use the Xbox Live Trace Analyzer tool on GNDP to analyze your title's use of Xbox Live Services. See the documentation included with the Xbox Live Trace Analyzer tool on Game Developer Network for a description of how to invoke the tool and interpret its output. You can also run XBLTraceAnalyzer.exe with the command line option of -? or -h to view command line help.
