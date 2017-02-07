---
author: drewbat
ms.assetid: 
title: Developing UWP apps with pre-release APIs
description: Understand the benefits and caveats of using pre-release APIs that are included in UWP SDK previews.
---

# Developing UWP apps with pre-release APIs

Microsoft publishes preview releases of the Universal Windows Platform (UWP) SDK to allow developers to engage with new platform features before they are finalized. This gives you a head start on incorporating features into your app and helps you to publish your app sooner after the official RTM version of the SDK is released. Using prerelease APIs also enables you to provide feedback to Microsoft to help influence the direction of the platform in future releases.

## Important limitations on the use of pre-release APIs
Before using a prerelease API in your app, be aware of the following important implications of doing so: 
* Apps that use prerelease APIs can’t be submitted to the Windows Store until the APIs are officially published in an RTM release. We strongly recommended that you keep prerelease development code separate from the code for your currently published apps. 
* Apps that you develop by using a preview SDK can’t be submitted to the Store even if dont't use any pre-release APIs. You should install the preview tools on a machine or VM that is separate from the production machine you use for your primary development. 
* Prerelease APIs are likely to change before RTM. When APIs are included in a preview SDK, it is likely that the feature or scenario they enable will be included in the final SDK. But the names, signatures, and behavior of specific APIs may change before the final release, and it is possible that the API will be removed entirely. 

## How to identify a prerelease API 
In the API reference documentation for UWP, APIs that are prerelease are labelled with the following text: 

This article discusses a prerelease API or feature that may be substantially modified before it’s commercially released. You can use this feature for development and testing right now, but apps that use it can’t be submitted to the Windows Store until after it is has been commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here. For more information on developing with prerelease APIs, see Developing UWP apps with prerelease APIs. 

## Get the latest SDK Insider preview 
1. [Sign up for the Windows Insider Program](https://insider.windows.com/) to get access to preview builds of the SDK. 
3. Before installing the developer preview tools, review the [release notes for current SDK and Mobile emulator](http://go.microsoft.com/fwlink/?LinkId=829180).
4. Install the [SDK Insider Preview](https://www.microsoft.com/en-us/software-download/windowsinsiderpreviewSDK).
5. Check out the [Windows Insider Preview Community forum](http://go.microsoft.com/fwlink/p/?LinkId=507620)
