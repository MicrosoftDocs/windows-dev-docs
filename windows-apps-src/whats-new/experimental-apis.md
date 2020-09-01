---
title: Experimental APIs
description: Learn how experimental APIs are flighted externally using Windows Insider SDKs, so developers can try them out.
ms.date: 11/13/2017
ms.topic: article
keywords: windows 10, uwp, experimental, api
ms.localizationpriority: medium
---
# Experimental APIs

Experimental APIs are in the early stages of design and are likely to change as the owners incorporate feedback and add support for additional scenarios.

These APIs are flighted externally using [Windows Insider SDKs](https://www.microsoft.com/software-download/windowsinsiderpreviewSDK) so that developers can try them out and provide feedback before they become part of the official platform. While they are flighted, there is no promise of stability or commitment.

## Consuming experimental APIs
Intellisense will let you know if an API is experimental. You will also get a compiler warning when you use an experimental API such as "... is for evaluation purposes only and is subject to change or removal in future updates".

These warnings help protect you from creating dependencies on experimental APIs in production projects. For experimental projects, you can ignore or disable these warnings.

By default, these APIs are disabled at runtime and calling them will result in a runtime exception. This is another safeguard to help prevent inadvertent dependencies and broad distribution of apps that consume experimental APIs.

To enable these APIs for experimentation, use the [Windows Device Portal (WDP)](../debug-test-perf/device-portal.md) Features plug-in on the target device to enable the feature corresponding to the API you want to call.

Documentation for a particular experimental API is at the discretion of the team that owns it.

## Providing feedback

If you've tried an experimental API and would like to provide feedback, use the **Developer Platform** category in the [Windows Feedback Hub](https://support.microsoft.com/help/4021566/windows-10-send-feedback-to-microsoft-with-feedback-hub).