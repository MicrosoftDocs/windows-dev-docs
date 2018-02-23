---
title: Using Xbox Live APIs built into the XDK
author: KevinAsgari
description: Learn how to use the built-in Xbox Live APIs in your Xbox Developer Kit (XDK) project.
ms.assetid: 539caca3-58bc-49d9-8432-ca8e57755be2
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Using Xbox Live APIs built into the XDK

1. Right click on your project in Visual Studio, and choose "References".
1. Choose "Add New Reference"
1. Click on "Durango.<build number>" and "Extensions" on the left panel
1. In the middle, choose either:
- If you want to use the WinRT XSAPI API, choose "Xbox Services API"
- If you want to use the C++ XSAPI API, choose "Xbox Services API Cpp"
1. Click OK

Note: If your build system doesn't support props files, you must manually add the preprocessor definitions and libraries as seen in
`%XboxOneExtensionSDKLatest%\ExtensionSDKs\Xbox.Services.API.Cpp\8.0\DesignTime\CommonConfiguration\Neutral\Xbox.Services.API.Cpp.props`
