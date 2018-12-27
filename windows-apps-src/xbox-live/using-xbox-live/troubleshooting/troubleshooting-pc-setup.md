---
title: Troubleshooting Xbox Live setup on Windows PC

description: Learn how to troubleshoot your Xbox Live development environment on a Windows PC.
ms.assetid: 9cfebdcd-0c1c-4fc2-9457-e7e434b6374c
ms.date: 04/04/2017
ms.topic: article
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, troubleshoot
ms.localizationpriority: medium
---
# Troubleshooting Xbox Live setup on Windows PC

On Windows 10 PC, you can ensure your machine is setup correctly with these steps:

1. Change your machine to point to the XDKS.1 sandbox where samples are designed to run.  Do this by running this script:

        {*SDK source root*}\Tools\SwitchSandbox.cmd XDKS.1

1. Extract the contents of the zip file "SourcesAndSamples.zip" found inside the SDK.
1. Open a sample solution:
    1. For C++ API: {*SDK source root*}\Samples\Social\UWP\Cpp\Social.Cpp.140.sln
    1. For WinRT API with C#: {*SDK source root*}\Samples\Social\UWP\CSharp\Social.CSharp.140.sln
    1. For WinRT API with C++/CX:  {*SDK source root*}\Samples\TitleStorage\UWP\CppCx\TitleStorageUniversal.sln
1. Change the build target platform to either "Win32" or "x64".
1. Right click the solution and re-build everything.
1. Launch the app in the debugger.
1. Sign-in with the development account that you created on the [Xbox Developer Portal](https://xdp.xboxlive.com), or with a retail developer account authorized in [Partner Center](https://partner.microsoft.com/dashboard).
1. Grant the app permission to access your Xbox Live information.
1. Verify that the app can retrieve your information and you can see your gamertag.