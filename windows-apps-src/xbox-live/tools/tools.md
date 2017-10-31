---
title: Tools
author: KevinAsgari
description: Learn about tools that are provided to help develop and test your Xbox Live enabled title.
ms.assetid: 380a29bf-41a7-4817-9c57-f48f2b824b52
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, tools
localizationpriority: medium
---

# Tools

This section covers various tools that you can use to help you use Xbox Live.

[Xbox Live Trace Analyzer](analyze-service-calls.md)  
The Xbox Live Services API now allows title developers to capture all service calls and then analyze them offline for any violations in calling patterns. Service call tracing can be activated by using new functionality available in the xbtrace command line tool, or through protocol activation for more advanced scenarios. Activating service call tracking directly from title code is also supported. The offline analysis tool, called the Xbox Live Trace Analyzer (XBLTraceAnalyzer.exe), is available on Game Developer Network.

[Xbox Live Account Tool](xbox-live-account-tool.md)   
The Xbox Live Account Tool is a tool designed to help title developers set up existing dev accounts for testing game scenarios. For example, you can use Xbox Live Account Tool to change a dev account's gamertag, or quickly add 1000 followers to an account's friends list.

[Xbox Live PowerShell Module](https://github.com/Microsoft/xbox-live-powershell-module/blob/master/docs/XboxLivePsModule.md)  
XboxlivePSModule contains various utilities to help Xbox Live development.
* To consume it from [PowerShell Gallery](https://www.powershellgallery.com/packages/XboxlivePSModule), open a PowerShell window:
    1. Download and install the module: `Install-Module XboxlivePSModule -Scope CurrentUser`
    2. Start using by running `Import-Module XboxlivePSModule`
    3. Run cmdlets, i.e. Set-XblSandbox XDKS.1, Get-XblSandbox

* To consume it from downloaded zip file, open a PowerShell window,
    1. Run `Import-Module <path to unzipped folder>\XboxLivePsModule\XboxLivePsModule.psd1`
    2. Run cmdlets, i.e. Set-XblSandbox XDKS.1, Get-XblSandbox

For more details for available cmdlets and usage, please visit online doc from [github](https://github.com/Microsoft/xbox-live-powershell-module/blob/master/docs/XboxLivePsModule.md)
