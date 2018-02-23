---
title: Compile the XDK Xbox Live API source
author: KevinAsgari
description: Learn how to compile the Xbox Live API source that is shipped with the Xbox Developer Kit (XDK).
ms.assetid: 78425e82-c132-4f6b-9db3-2536862f1ce5
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, xdk
ms.localizationpriority: low
---

# Compile the Xbox Developer Kit (XDK) Xbox Live API source

The Xbox Developer Kit (XDK) includes source for building the Microsoft.Xbox.Services.dll (XSAPI). Developers can follow these instructions to update their projects to use a local build of the DLL.

You might want to build XSAPI yourself if:
1. If you want to debug an issue to understand where an error code is coming from.
1. If we provide a source code patch to fix an issue for you, before we can distribute a QFE.

## To compile the XDK C++ XSAPI project for yourself

<ol>
  <li> Get the Microsoft.Xbox.Services source. To do this, extract all files from "%XboxOneExtensionSDKLatest%\ExtensionSDKs\Xbox Services API\8.0\SourceDist\Xbox.Services.zip" to a writable folder outside of "C:\Program Files (x86)", or you can clone the source from <a href ="https://github.com/Microsoft/xbox-live-api">https://github.com/Microsoft/xbox-live-api</a></li>
  <li> If your project references the pre-build DLL, you need to remove the reference</li>
    <ul>
      <li> For Visual Studio 2012: Choose “Project->References...” in Visual Studio. If Xbox Services API is listed as a reference, select it and click “Remove Reference”. Click “OK” and save the project file.</li>
      <li> For Visual Studio 2015 or 2017: Choose “Project->Add References…” in Visual Studio. If Xbox Services API is checked, uncheck it. Click “OK” and save the project file.</li>
    </ul>
  <li> If you are building with the XDK, choose “File->Add->Existing Project…” in Visual Studio to add the following two projects to your application's solution. The vcxproj files will be located in the folder you extracted the source to.</li>
    For Visual Studio 2017:
    <ul>
      <li>\Build\Microsoft.Xbox.Services.141.XDK.Cpp\Microsoft.Xbox.Services.141.XDK.Cpp.vcxproj</li>   <li>\External\cpprestsdk\Release\src\build\vs15.xbox\casablanca141.Xbox.vcxproj</li>
    </ul>
    For Visual Studio 2015:
    <ul>
      <li>\Build\Microsoft.Xbox.Services.140.XDK.Cpp\Microsoft.Xbox.Services.140.XDK.Cpp.vcxproj</li> <li>\External\cpprestsdk\Release\src\build\vs14.xbox\casablanca140.Xbox.vcxproj</li>
    </ul>
    For Visual Studio 2012:
    <ul>
      <li>\Build\Microsoft.Xbox.Services.110.XDK.Cpp\Microsoft.Xbox.Services.110.XDK.Cpp.vcxproj</li> <li>\External\cpprestsdk\Release\src\build\vs11.xbox\casablanca110.Xbox.vcxproj</li>
    </ul>
    <li> Add the source projects as a reference by choosing Project->References... and select "Add Reference". Under "Solution->Projects", check the entries for both projects above then click OK.</li>
    <li> Add the props file to your project by clicking "View->Other Windows->Property Manager", right clicking on your project, selecting "Add Existing Property Sheet", then finally selecting the xsapi.staticlib.props file in the SDK sourch root.  If your build system doesn’t support props files, you must manually add the preprocessor definitions and libraries as seen in %XboxOneExtensionSDKLatest%\ExtensionSDKs\Xbox.Services.API.Cpp\8.0\DesignTime\CommonConfiguration\Neutral\Xbox.Services.API.Cpp.props</li>
    <li> Add the services.h file to your app source by right clicking on the project Add->Existing Item and choosing the {SDK source root}\Include\xsapi\services.h file</li>
    <li> Ensure that the "Output Folder" of both the application project and the Xbox Services Project are the same. This setting can be found in Visual Studio project Properties->Configuration Properties->General->Output Directory.</li>
    <li> Rebuild your Visual Studio solution</li>
</ol>

## To compile the XDK WinRT XSAPI project for yourself

<ol>
  <li> Get the Microsoft.Xbox.Services source. To do this, you extract all files from "%XboxOneExtensionSDKLatest%\ExtensionSDKs\Xbox Services API\8.0\SourceDist\Xbox.Services.zip" to a writable folder outside of "C:\Program Files (x86)", or you can clone the source from <a href ="https://github.com/Microsoft/xbox-live-api">https://github.com/Microsoft/xbox-live-api</a></li>
  <li> If your project references the pre-build DLL, you need to remove the reference</li>
    <ul>
      <li> For Visual Studio 2012: Choose “Project->References...” in Visual Studio. If Xbox Services API is listed as a reference, select it and click “Remove Reference”. Click “OK” and save the project file.</li>
      <li> For Visual Studio 2015 or 2017: Choose “Project->Add References…” in Visual Studio. If Xbox Services API is checked, uncheck it. Click “OK” and save the project file.</li>
    </ul>
  <li> If you are building with the XDK, choose “File->Add->Existing Project…” in Visual Studio to add the following two projects to your application's solution. The vcxproj files will be located in the folder you extracted the source to.  For Visual Studio 2015, the projects will automatically upgrade to the VS2015 format.</li>
    <ul>
      <li>\Build\Microsoft.Xbox.Services.110.XDK.WinRT\Microsoft.Xbox.Services.110.XDK.WinRT.vcxproj</li> <li>\External\cpprestsdk\Release\src\build\vs11.xbox\casablanca110.Xbox.vcxproj</li>
    </ul>
  <li> In Visual Studio add the references:</li>
    <ul>
      <li> For Visual Studio 2012: Choose “Project->References...” and select “Add Reference” in Visual Studio. Under Solution->Projects, chceck the entries for both projects above and click OK.</li>
      <li> For Visual Studio 2015 or 2017: Choose “Project->Add References…” in Visual Studio. Under Projects, check the entries for both projects above and click OK.</li>
    </ul>
  <li> Ensure that the "Output Folder" of both the application project and the Xbox Services Project are the same. This setting can be found in Visual Studio project Properties->Configuration Properties->General->Output Directory.</li>
  <li> Rebuild your Visual Studio solution</li>
</ol>
